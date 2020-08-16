using System.Collections;

using NGTT.Bases.Abstracts;
using NGTT.Helpers;
using UnityEngine;

namespace NGTT.Entities
{
    public class SimpleBomb : ProjectileBase
    {
        private bool _isCollided;
        private MeshRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material.color = Color.black;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isCollided) return;
            Detonate();
            _isCollided = true;
        }

        internal override void Detonate()
        {
            tag = "activated";
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