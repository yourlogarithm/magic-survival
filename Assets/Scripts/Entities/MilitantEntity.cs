using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public abstract class MilitantEntity : Entity
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float knockbackForce;
        
        private bool _cooledDown;
        private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
        
        protected float KnockbackForce
        {
            get => knockbackForce;
        }

        protected bool CooledDown
        {
            get => _cooledDown;
        }
        

        protected override void Update()
        {
            base.Update();
            Combat();
        }

        private void Combat()
        {
            if (Dead || _cooledDown || !CanAttack())
                return;
                
            Animator_.SetTrigger(AttackTrigger);
            StartCoroutine(CooldownAttack());
            OnAttack();
        }

        protected abstract void OnAttack();

        protected abstract bool CanAttack();

        private IEnumerator<WaitForSeconds> CooldownAttack()
        {
            _cooledDown = true;
            yield return new WaitForSeconds(attackCooldown);
            _cooledDown = false;
        }
    }
}