using Harmony;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(EnemyMelee))]
    public class Torchman : Enemy
    {
        private EnemyMelee meleeWeapon;
        private Animator animator;
        private new Collider2D collider2D;
        private new Audio audio;

        protected override void Awake()
        {
            base.Awake();
            meleeWeapon = GetComponent<EnemyMelee>();
            animator = GetComponentInChildren<Animator>();
            collider2D = GetComponentInChildren<Collider2D>();
            audio = Finder.Audio;
        }

        private void Update()
        {
            if (IsStopped) return;
            
            if (meleeWeapon.IsInRange)
            {
                Mover.StopWalking();
                Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                animator.SetBool(AnimatorParameters.isAttacking, true);
                meleeWeapon.Attack();
                StopMovement();
            }
        }

        public void OnAttackAnimationOver()
        {
            Die();
        }

        public override void Die()
        {
            base.Die();
            animator.SetBool(AnimatorParameters.idle, true);
            audio.MainSoundPlayer.Play(audio.GameClips.torchmanDeath);
        }

        protected override void RemoveEnemy()
        {
            EnemyManager.RemoveTorchman(this);
        }
    }
}