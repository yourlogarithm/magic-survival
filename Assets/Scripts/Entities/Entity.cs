using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected float health;
        [SerializeField] protected float speed;
        [SerializeField] protected float damage;
        [SerializeField] protected float destroyDelay = 3f;


        // ReSharper disable once InconsistentNaming
        protected SpriteRenderer SpriteRenderer_;
        // ReSharper disable once InconsistentNaming
        protected Animator Animator_;
        // ReSharper disable once InconsistentNaming
        protected Renderer Renderer_;
        // ReSharper disable once InconsistentNaming
        protected Rigidbody2D Rigidbody2D_;
        // ReSharper disable once InconsistentNaming
        protected AudioSource AudioSource_;

        private float _knockbackCooldown = 0.1f;
        private float _knockbackTime;
        private float _deathAnimationClipLength;
        private bool _dead;
        
        public bool Dead
        {
            get => _dead;
        }

        protected float KnockbackTime
        {
            get => _knockbackTime;
        }
        
        protected static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsDying = Animator.StringToHash("isDying");

        protected virtual void Update()
        {
            if (_knockbackTime <= 0)
                Rigidbody2D_.velocity = Vector2.zero;
            else
                _knockbackTime -= Time.deltaTime;
        }

        protected virtual void Awake()
        {
            Animator_ = GetComponent<Animator>();
            Renderer_ = GetComponent<Renderer>();
            Rigidbody2D_ = GetComponent<Rigidbody2D>();
            SpriteRenderer_ = GetComponent<SpriteRenderer>();
            AudioSource_ = GetComponent<AudioSource>();
            AnimationClip clip = Animator_.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "death");
            if (clip != null)
                _deathAnimationClipLength = clip.length;
            else
                _deathAnimationClipLength = 0f;
        }

        IEnumerator<WaitForSeconds> PaintRed()
        {
            Renderer_.material.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            Renderer_.material.color = Color.white;
        }

        public virtual void TakeHit(Hit hit)
        {
            health -= hit.Damage;
            ApplyKnockback(hit);
            StartCoroutine(PaintRed());
            AudioSource_.Play();
            if (health <= 0 && !_dead)
                Die();
        }

        private void ApplyKnockback(Hit hit)
        {
            Rigidbody2D_.AddForce(hit.Knockback, ForceMode2D.Impulse);
            _knockbackTime = _knockbackCooldown;
        }

        protected virtual void Die()
        {
            _dead = true;
            Animator_.SetBool(IsDying, true);
            Destroy(gameObject, _deathAnimationClipLength + destroyDelay);
        }
    }
}
