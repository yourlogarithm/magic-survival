using System;
using Entities;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public float speed = 50f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float lifeTime = 2f;
    
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
    
        private static readonly int ExplodeTrigger = Animator.StringToHash("ExplodeTrigger");

        public void Launch(Vector2 direction)
        {
            _rigidbody2D.velocity = direction * speed;
        }
        
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            Destroy(gameObject, lifeTime);
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                _rigidbody2D.velocity = transform.right * 0;
                _animator.SetTrigger(ExplodeTrigger);
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject, 1f);
        }
    }
}
