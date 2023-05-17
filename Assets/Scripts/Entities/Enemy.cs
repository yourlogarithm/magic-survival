using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private DamagePopup damagePopupPrefab;
        [SerializeField] protected float viewDistance;
        [SerializeField] private Canvas canvas;
        
        private Transform _player;
        private bool _triggered;
        
        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
            CheckIfNearPlayer();
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_triggered)
                MoveTowardsPlayer();
        }
        
        void CheckIfNearPlayer()
        {
            if (Vector3.Distance(transform.position, _player.position) <= viewDistance)
                _triggered = true;
        }

        void MoveTowardsPlayer()
        {
            Vector2 toPlayer = (_player.position - transform.position).normalized;
            Rigidbody2D.MovePosition(Rigidbody2D.position + toPlayer * (speed * Time.deltaTime));
            Animator.SetBool(IsRunning, true);
            if (toPlayer.x > 0)
                SpriteRenderer.flipX = false;
            else if (toPlayer.x < 0)
                SpriteRenderer.flipX = true;
        }
        
        
        public override void TakeDamage(float value)
        {
            base.TakeDamage(value);
            ShowDamagePopup(value);
        }
        
        private void ShowDamagePopup(float value)
        {
            DamagePopup damagePopup = Instantiate(damagePopupPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity, canvas.transform);
            damagePopup.Setup(value);
        }
    }
}
