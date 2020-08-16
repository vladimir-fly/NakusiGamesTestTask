using System.Collections;
using NGTT.Bases.Abstracts;
using NGTT.Helpers;
using UnityEngine;

namespace NGTT.Entities
{
    public class TimeBomb : ProjectileBase
    {
        private float _countdown;
        private float _elapsedTime;

        private MeshRenderer _renderer;
        private void Start()
        {
            _countdown = Defaults.ProjectileCountdown;
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material.color = Color.blue;
        }

        private void Update()
        {
            if (_elapsedTime >= _countdown)
            {
                Detonate();
                _elapsedTime = 0;
            }
            _elapsedTime += Time.deltaTime;
        }

        internal override void Detonate()
        {
            tag = Defaults.ProjectileActivatedTag;
            _renderer.material.color = Color.red;
            Destroy(gameObject.GetComponent<Rigidbody>());
            transform.localScale *= Defaults.ProjectileExplosionScale;
            StartCoroutine(DestroyDelay(Defaults.ProjectileDestroyDelay));
        }
        
        IEnumerator DestroyDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}