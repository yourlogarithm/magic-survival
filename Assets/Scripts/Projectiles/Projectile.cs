using Combat;
using Entities;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public float speed = 50f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private float knockbackForce = 10f;
    
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private bool _isExploding;
    
        private static readonly int ExplodeTrigger = Animator.StringToHash("ExplodeTrigger");

        public float Damage
        {
            get => damage;
            set => damage = value;
        }
        
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            Destroy(gameObject, lifeTime);
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody2D.velocity = direction * speed;
        }
        

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
                Explode(enemy);
            Destroy(gameObject, 1f);
        }

        private void Explode(Enemy enemy)
        {
            if (_isExploding)
                return;
            Hit hit = new Hit(damage, _rigidbody2D.velocity.normalized * knockbackForce, null);
            _rigidbody2D.velocity = transform.right * 0;
            _animator.SetTrigger(ExplodeTrigger);
            enemy.TakeHit(hit);
            _isExploding = true;
        }
    }
}
