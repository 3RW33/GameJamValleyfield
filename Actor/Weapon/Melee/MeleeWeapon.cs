using UnityEngine;

namespace Game
{
    public abstract class MeleeWeapon : Weapon
    {
        [SerializeField] private float attackRange = 0.2f;

        protected float AttackRange => attackRange;

        public abstract void Attack();
    }
}