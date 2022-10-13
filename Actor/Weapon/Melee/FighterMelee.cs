using UnityEngine;

namespace Game
{
    public class FighterMelee : MeleeWeapon
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask targetLayers;

        private void Awake()
        {
            IsFriendly = true;
        }

        public override void Attack()
        {
            var hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, targetLayers);

            foreach (var target in hitTargets)
            {
                target.GetComponentInParent<IHittable>()?.Hit(Damage);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
                Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
        }
#endif
    }
}