using System.Collections;
using Harmony;
using UnityEngine;

namespace Game
{
    public class Mage : Enemy
    {
        [SerializeField] private float range = 3f;
        [SerializeField] private GameObject shotPoint;
        
        public MageProjectile MageProjectile { get; set; }
        public Vector2 ShotPointPosition => shotPoint.transform.position;
        
        private Animator animator;
        private new Audio audio;

        protected override void Awake()
        {
            base.Awake();
            MageProjectile = GetComponentInChildren<MageProjectile>();
            animator = GetComponentInChildren<Animator>();
            audio = Finder.Audio;
        }

        private void Update()
        {
            if (IsStopped) return;
            
            if (Vector2.Distance(transform.position, Cart.transform.position) <= range && !MageProjectile.IsShot)
            {
                animator.SetBool(AnimatorParameters.isAttacking, true);
                StopMovement();
                MageProjectile.Shoot(Direction);
            }
        }

        public override void Die()
        {
            base.Die();
            animator.SetBool(AnimatorParameters.idle, true);
            audio.MainSoundPlayer.Play(audio.GameClips.mageDeath);
        }

        protected override void RemoveEnemy()
        {
            EnemyManager.RemoveMage(this);
        }

        public override void Hit(int hitPoints)
        {
            Die();
        }
    }
}