using System;
using UnityEngine;
using NGTT.Bases.Abstracts;
using NGTT.Helpers;
using Random = UnityEngine.Random;

namespace NGTT.Entities
{
    public class SimpleUnit : UnitBase
    {
        private Vector3 _target;
        private float _targetCooldown;
        private float _elapsedTime;

        public Action<SimpleUnit> OnHit;
        
        private void Start()
        {
            Health = Defaults.UnitHealth;
            _targetCooldown = Random.Range(1, 10);
            GenerateTarget();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_elapsedTime >= _targetCooldown)
            {
                GenerateTarget();
                _elapsedTime = 0;
            }
            else
            {
                transform.localPosition =
                    Vector3.Lerp(transform.localPosition, _target, Time.deltaTime);
                _elapsedTime += Time.deltaTime;
            }
        }

        private void GenerateTarget()
        {
            _target =
                new Vector3(
                    Random.Range(-10.1f, 10.1f) * Random.Range(-100, 100) * Time.deltaTime,
                    0,
                    Random.Range(-10.1f, 10.1f) * Random.Range(-100, 100) * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.name.Contains("Bomb")) return;
            Physics.Raycast(transform.position, other.transform.position, out var hit);

            var hitCollider = hit.collider;
            if (hitCollider == null) return;
            
            if (!hitCollider.name.Contains("Barrier") && hitCollider.CompareTag(Defaults.ProjectileActivatedTag))
                OnHit?.Invoke(this);
        }
    }
}