using Game;
using Harmony;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class ArcherAim : MonoBehaviour
    {
        [SerializeField] private Transform aimOriginTransform;
        [SerializeField] private Transform bowTransform;
        
        private Archer archer;
        private new Camera camera;
        private Inputs inputs;
        private Vector2 neutralAimDirection;
        private Vector2 cursorPosition; // In world space

        private Vector2 MouseScreenPosition => inputs.Actions.Archer.Point.ReadValue<Vector2>();
        public Vector2 AimDirection { get; private set; }
        public Vector3 AimOrigin => aimOriginTransform.position;
        
        public float Angle { get; private set; }

        private void Awake()
        {
            archer = GetComponent<Archer>();
            camera = Camera.main;
            inputs = Finder.Inputs;
            neutralAimDirection = Vector2.right;
            cursorPosition = Vector2.zero;
            AimDirection = neutralAimDirection;
        }

        private void Update()
        {
            if (inputs.CurrentControlScheme == inputs.Actions.KeyboardMouseScheme)
            {
                UpdateMousePosition();
                AimTowardsTarget(cursorPosition);
            }
        }

        public void SubscribeToInputs()
        {
            inputs.Actions.Archer.Aim.performed += OnAimPerformed;
        }

        public void UnsubscribeToInputs()
        {
            inputs.Actions.Archer.Aim.performed -= OnAimPerformed;
        }

        private void OnAimPerformed(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                AimTowardsDirection(direction);
            }
        }

        private void UpdateMousePosition()
        {
            if (!archer.IsBeingControlled) return;
            
            var cursorScreenPosition = MouseScreenPosition;
            if (cursorScreenPosition != Vector2.zero)
            {
                cursorPosition = camera.ScreenToWorldPoint(cursorScreenPosition);
            }
        }

        private void AimTowardsDirection(Vector2 direction)
        {
            AimDirection = direction.normalized;
            
            // TODO: Turn arm, add min and max angle to not aim backwards

            Angle = CalculateAimAngle() - 180;
            if (Angle > -90 && Angle < 90)
            {
                bowTransform.rotation = Quaternion.Euler(0, 0, Angle);
            }
        }

        private void AimTowardsTarget(Vector2 target)
        {
            AimTowardsDirection(AimOrigin.DirectionTo(target));
        }

        public float CalculateAimAngle()
        {
            float angle = Mathf.Rad2Deg * Mathf.Acos(AimDirection.x);
            if (AimDirection.y < 0)
            {
                // We subtract 360 by the angle since a value has 2 angle in the trigonometry circle
                return 360 - angle;
            }

            return angle;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (EditorApplication.isPlaying)
            {
                // Aiming vector
                var position = AimOrigin;
                GizmosExtensions.DrawLine(
                    (Vector2) position,
                    (Vector2) position + AimDirection,
                    Color.red
                );

                // Mouse position
                Handles.DrawSolidDisc(cursorPosition, Vector3.forward, 0.1f);
            }
        }
#endif
    }
}