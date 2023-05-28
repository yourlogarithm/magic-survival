using Combat;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class Enemy : MilitantEntity
    {
        [SerializeField] private DamagePopup damagePopupPrefab;
        [SerializeField] protected float viewDistance;
        [SerializeField] public Canvas canvas;
        [SerializeField] public TextMeshProUGUI killCountText;
        
        private Player _player;
        private bool _triggered;
        private bool _canAttack;
        private bool _playerDead;
        
        protected override void Awake()
        {
            base.Awake();
            _player = FindObjectOfType<Player>();
        }

        protected override void Update()
        {
            base.Update();
            CheckIfNearPlayer();
        }
        
        protected void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _canAttack = true;
            }
        }

        protected void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _canAttack = false;
            }
        }

        void CheckIfNearPlayer()
        {
            if (!_playerDead && Vector3.Distance(transform.position, _player.transform.position) <= viewDistance)
                _triggered = true;
        }

        void MoveTowardsPlayer()
        {
            if (Dead || _playerDead || !_triggered || KnockbackTime > 0 || CooledDown)
            {
                Animator_.SetBool(IsRunning, false);
                return;
            }
            Vector2 toPlayer = (_player.transform.position - transform.position).normalized;
            Rigidbody2D_.MovePosition(Rigidbody2D_.position + toPlayer * (speed * Time.deltaTime));
            Animator_.SetBool(IsRunning, true);
            if (toPlayer.x > 0)
                SpriteRenderer_.flipX = false;
            else if (toPlayer.x < 0)
                SpriteRenderer_.flipX = true;
        }
        
        public override void TakeHit(Hit hit)
        {
            base.TakeHit(hit);
            ShowDamagePopup(hit.Damage);
        }
        
        private void ShowDamagePopup(float value)
        {
            DamagePopup damagePopup = Instantiate(damagePopupPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity, canvas.transform);
            damagePopup.Setup(value);
        }

        protected override bool CanAttack()
        {
            return _canAttack;
        }
        
        protected override void OnAttack()
        {
            if (!_player.Dead)
            {
                Vector2 push = (_player.transform.position - transform.position).normalized * KnockbackForce;
                _player.TakeHit(new Hit(10, push, null));
            }
            else
            {
                _playerDead = true;
            }
        }

        protected override void Die()
        {
            base.Die();
            killCountText.text = (int.Parse(killCountText.text) + 1).ToString();
        }
    }
}
