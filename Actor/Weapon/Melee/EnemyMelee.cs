using System;
using Harmony;
using UnityEngine;

namespace Game
{
    public class EnemyMelee : MeleeWeapon
    {
        private Cart cart;
        private Enemy enemy;
        private new Collider2D collider2D;

        private Vector2 Size => collider2D.bounds.size;
        private float DistanceToCart => Math.Abs(transform.position.x - cart.transform.position.x)
                                        - cart.Size.x / 2
                                        - Size.x / 2;
        public bool IsInRange => DistanceToCart < AttackRange;

        private void Awake()
        {
            cart = Finder.Cart;
            enemy = GetComponentInParent<Enemy>();
            collider2D = GetComponentInChildren<Collider2D>();
            IsFriendly = false;
        }

        public override void Attack()
        {
            cart.Hit(Damage);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (collider2D == null)
            {
                collider2D = GetComponentInChildren<Collider2D>();
            }
            
            var distance = Size.x + AttackRange;
            var offset = Vector3.zero;
            offset.x += distance;
            var origin = collider2D.bounds.center;
            Gizmos.DrawLine(origin - offset, origin + offset);
        }
#endif
    }
}