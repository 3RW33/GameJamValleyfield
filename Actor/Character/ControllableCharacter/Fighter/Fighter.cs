using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(FighterMelee))]
    public class Fighter : ControllableCharacter
    {
        private Animator animator;
        private FighterMelee meleeWeapon;
        private bool movePressed;
        private new Audio audio;

        protected override void Awake()
        {
            base.Awake();
            meleeWeapon = GetComponent<FighterMelee>();
            animator = GetComponentInChildren<Animator>();
            audio = Finder.Audio;
        }

        private void OnEnable()
        {
            SubscribeToMovementInputs();
        }

        private void OnDisable()
        {
            UnsubscribeToMovementInputs();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            movePressed = true;
            if (!IsBeingControlled) return;

            Move(context.ReadValue<float>());
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            Mover.CanWalk = false;
            animator.SetTrigger(AnimatorParameters.swordmanAttack);
            meleeWeapon.Attack();
            audio.MainSoundPlayer.Play(audio.GameClips.swordSwing);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            movePressed = false;

            StopMoving();
        }

        public void OnAttackAnimationOver()
        {
            if (IsBeingControlled)
                Mover.CanWalk = true;
        }

        private void Move(float value)
        {
            animator.SetTrigger(AnimatorParameters.startWalking);
            Mover.StartWalking(value);
        }

        private void StopMoving()
        {
            animator.SetTrigger(AnimatorParameters.stopWalking);
            Mover.StopWalking();
        }

        private void SubscribeToMovementInputs()
        {
            Inputs.Actions.Fighter.Move.performed += OnMovePerformed;
            Inputs.Actions.Fighter.Move.canceled += OnMoveCanceled;
        }

        private void UnsubscribeToMovementInputs()
        {
            Inputs.Actions.Fighter.Move.performed -= OnMovePerformed;
            Inputs.Actions.Fighter.Move.canceled -= OnMoveCanceled;
        }

        private void SubscribeToAttackInputs()
        {
            Inputs.Actions.Fighter.Attack.started += OnAttack;
        }

        private void UnsubscribeToAttackInputs()
        {
            Inputs.Actions.Fighter.Attack.started -= OnAttack;
        }

        public override void StartControlling()
        {
            base.StartControlling();
            SubscribeToAttackInputs();
            
            if (movePressed) Move(Inputs.Actions.Fighter.Move.ReadValue<float>());
            
            audio.MainSoundPlayer.Play(audio.GameClips.switchToSwordman);
        }

        public override void StopControlling()
        {
            base.StopControlling();
            UnsubscribeToAttackInputs();
            
            StopMoving();
        }
    }
}