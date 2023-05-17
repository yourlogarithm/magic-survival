using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected float health = 100;
        [SerializeField] protected float speed = 5;
        [SerializeField] protected float damage = 3;
        [SerializeField] protected float deathAnimationDuration;
        
        protected SpriteRenderer SpriteRenderer;
        protected Animator Animator;
        protected Renderer Renderer;
        protected Rigidbody2D Rigidbody2D;
        
        protected static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int DieTrigger = Animator.StringToHash("DieTrigger");

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            Renderer = GetComponent<Renderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void FixedUpdate()
        {
        }

        IEnumerator<WaitForSeconds> ResetColor(float delay)
        {
            yield return new WaitForSeconds(delay);
            Renderer.material.color = Color.white;
        }

        public virtual void TakeDamage(float value)
        {
            health -= value;
            Renderer.material.color = Color.red;
            StartCoroutine(ResetColor(0.25f));
            if (health <= 0)
                Die();
        }

        void Die()
        {
            Animator.SetTrigger(DieTrigger);
            Destroy(gameObject, deathAnimationDuration);
        }
    }
}
