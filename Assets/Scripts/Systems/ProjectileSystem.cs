using System;
using System.Collections;
using UnityEngine;

using NGTT.Bases.Abstracts;
using NGTT.Bases.Enums;
using NGTT.Bases.Headers;
using NGTT.Entities;
using NGTT.Helpers;
using NGTT.Messages;
using Random = UnityEngine.Random;

namespace NGTT.Systems
{
    public class ProjectileSystem : SystemBase
    {
        private Vector3 _spawnPoint;
        private bool _working;
        private float _cooldown;
        private float _time;
        private int _projectilesCount;

        [SerializeField] private EProjectileType _currentProjectileType;
        
        private void Start()
        {
            _cooldown = Defaults.ProjectileSpawnCooldown;
            _spawnPoint = Defaults.ProjectileSpawnPoint;
        }

        public override void Launch()
        {
            _working = true;
        }

        public override void Halt()
        {
            _working = false;
        }

        public override void ReceiveMessage(IMessage message)
        {
            Debug.Log(LogExtension.Bold($"[ProjectileSystem][ReceiveMessage] {message}"));
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
        
        void Update()
        {
            if (!_working) return;
            if (_time >= _cooldown)
            {
                SpawnProjectile(_currentProjectileType);
                _time = 0;
                return;
            }

            _time += Time.deltaTime;
        }

        private void SpawnProjectile(EProjectileType projectileType)
        {
            ProjectileBase projectile = null; 
            
            switch (projectileType)
            {
                case EProjectileType.SimpleBomb:
                    projectile = GameObjectFactory.GetPrimitive<SimpleBomb>(PrimitiveType.Sphere);
                    break;
                case EProjectileType.TimeBomb:
                    projectile = GameObjectFactory.GetPrimitive<TimeBomb>(PrimitiveType.Sphere);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(projectileType), projectileType, null);
            }

            projectile.name += _projectilesCount;
            projectile.transform.position = _spawnPoint;
            var rb = projectile.gameObject.AddComponent<Rigidbody>();
            rb.mass = Defaults.ProjectileMass;
            rb.AddForce(Random.Range(-1000, 1000), 0, Random.Range(-1000, 1000));
        }
    }
}