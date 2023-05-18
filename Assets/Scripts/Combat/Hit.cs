using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public record Hit
    {
        public float Damage;
        public Vector2 Knockback;
        public List<StatusEffect> StatusEffects;

        public Hit(float damage, Vector2 knockback, List<StatusEffect> statusEffects)
        {
            Damage = damage;
            Knockback = knockback;
            StatusEffects = statusEffects;
        }
    }
}