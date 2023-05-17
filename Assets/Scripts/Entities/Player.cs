using System.Collections.Generic;
using Projectiles;
using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Camera mainCamera;


        private bool _canAttack = true;
        private float _moveInputX;
        private float _moveInputY;
    
        private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");

        private void Update()
        {
            Move();
            Combat();
            Rigidbody2D.velocity = Vector2.zero;
        }

        private void Move()
        {
            _moveInputX = Input.GetAxisRaw("Horizontal");
            _moveInputY = Input.GetAxisRaw("Vertical");
        
            if (_moveInputX != 0 || _moveInputY != 0)
                Animator.SetBool(IsRunning, true);
            else
                Animator.SetBool(IsRunning, false);
        
            if (_moveInputX > 0)
                SpriteRenderer.flipX = false;
            else if (_moveInputX < 0)
                SpriteRenderer.flipX = true;
            transform.position += new Vector3(_moveInputX, _moveInputY, 0f) * (speed * Time.deltaTime);
        }

        private void Combat()
        {
            if (Input.GetButtonDown("Fire1") && _canAttack)
            {
                Animator.SetTrigger(AttackTrigger);
                StartCoroutine(CooldownAttack(cooldown));
                StartCoroutine(ShootAfterDelay(0.5f));
            }
        }

        private IEnumerator<WaitForSeconds> ShootAfterDelay(float delay)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            yield return new WaitForSeconds(delay);
            Vector3 firePointPosition = firePoint.position;
            Vector2 direction = (mousePosition - firePointPosition).normalized;
            Projectile projectile = Instantiate(projectilePrefab, firePointPosition, firePoint.rotation);
            projectile.Launch(direction);
        }
        
        private IEnumerator<WaitForSeconds> CooldownAttack(float delay)
        {
            _canAttack = false;
            yield return new WaitForSeconds(delay);
            _canAttack = true;
        }
    }
}
