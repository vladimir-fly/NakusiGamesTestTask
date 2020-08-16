using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using NGTT.Bases.Abstracts;
using NGTT.Bases.Headers;
using NGTT.Entities;
using NGTT.Helpers;
using NGTT.Messages;

namespace NGTT.Systems
{
    public class UnitSystem : SystemBase
    {
        [SerializeField] private int _unitPoolSize;
        [SerializeField] private List<UnitBase> _units;
        [SerializeField] private GameObject _groundPlane;

        public override void Launch()
        {
            Debug.Log(LogExtension.Bold("[UnitSystem][Launch]"));
            
            _groundPlane = LocationGenerator.Generate();
            
            _units = new List<UnitBase>();
            _unitPoolSize = Defaults.UnitPoolSize;
            
            for (var i = 0; i < _unitPoolSize; i++)
                SpawnUnit();
        }

        public override void Halt()
        {
            Debug.Log(LogExtension.Bold("[UnitSystem][Halt]"));
            _units.ForEach(unit => Destroy(unit.gameObject));
            Destroy(_groundPlane);
        }
        
        public override void ReceiveMessage(IMessage message)
        {
            Debug.Log(LogExtension.Bold($" [UnitSystem][ReceiveMessage] {message}"));
            base.ReceiveMessage(message);
            switch (message)
            {
                case LaunchMessage _:
                    Launch();
                    break;
                case HaltMessage _:
                    Halt();
                    break;
            }
        }

        private void SpawnUnit()
        {
            var bounds = _groundPlane.GetComponent<MeshRenderer>().bounds;
            var unitObject = GameObjectFactory.GetPrimitive<SimpleUnit>(PrimitiveType.Cube);

            unitObject.name += _units.Count.ToString();
            _units.Add(unitObject);

            var unitPosition = 
                new Vector3(Random.Range(bounds.min.x, bounds.max.x), 
                    1, Random.Range(bounds.min.z, bounds.max.z));
            
            unitObject.transform.position = unitPosition;
            unitObject.gameObject.AddComponent<Rigidbody>();
            unitObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

            unitObject.OnHit = unit =>
            {
                if (unit.Health <= Defaults.ProjectileDamage)
                    Destroy(unit.gameObject);

                unit.Health -= Defaults.ProjectileDamage;

                var message = unit.Health > 0
                    ? $"{unit.name}: {unit.Health}"
                    : LogExtension.BoldRed($"{unit.name}: {unit.Health}");

                SendMessage(new UnitDataUpdateMessage {Message = message} );
            };
        }
    }
}