using System.Collections.Generic;
using Combat;
using Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class Player : MilitantEntity
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject healthBar;

        private float _moveInputX;
        private float _moveInputY;
        
        private Slider _healthBarSlider;

        protected override void Awake()
        {
            base.Awake();
            _healthBarSlider = healthBar.GetComponent<Slider>();
            _healthBarSlider.maxValue = health;
            _healthBarSlider.value = health;
        }
        
        protected void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (Dead)
                return;
            
            _moveInputX = Input.GetAxisRaw("Horizontal");
            _moveInputY = Input.GetAxisRaw("Vertical");
        
            if (_moveInputX != 0 || _moveInputY != 0)
                Animator_.SetBool(IsRunning, true);
            else
                Animator_.SetBool(IsRunning, false);
        
            if (_moveInputX > 0)
                SpriteRenderer_.flipX = false;
            else if (_moveInputX < 0)
                SpriteRenderer_.flipX = true;
            if (_moveInputX != 0 && _moveInputY != 0)
            {
                _moveInputX /= 1.5f;
                _moveInputY /= 1.5f;
            }
            transform.position += new Vector3(_moveInputX, _moveInputY, 0f) * (speed * Time.deltaTime);
        }

        protected override bool CanAttack()
        {
            return Input.GetButtonDown("Fire1");
        }
        
        protected override void OnAttack()
        {
            StartCoroutine(ShootAfterDelay(0.5f));
        }

        private IEnumerator<WaitForSeconds> ShootAfterDelay(float delay)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            yield return new WaitForSeconds(delay);
            Vector3 firePointPosition = firePoint.position;
            Vector2 direction = (mousePosition - firePointPosition).normalized;
            Projectile projectile = Instantiate(projectilePrefab, firePointPosition, firePoint.rotation);
            projectile.Damage += damage;
            projectile.Launch(direction);
        }
        
        public override void TakeHit(Hit hit)
        {
            base.TakeHit(hit);
            _healthBarSlider.value = health;
        }

        protected override void Die()
        {
            base.Die();
            Debug.Log("death, will end game after " + destroyDelay + " seconds");
            StartCoroutine(FindObjectOfType<GameStateManager>().EndGame(destroyDelay));
        }
    }
}
