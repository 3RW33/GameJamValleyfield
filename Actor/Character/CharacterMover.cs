using System;
using Harmony;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 3.5f;
        [SerializeField] private float maxVelocityMagnitude = 10;
        
        private const float VelocityErrorMargin = 0.0001f;

        private new Rigidbody2D rigidbody2D;
        private RigidbodyConstraints2D originalConstraints;
        private float targetWalkVelocity;
        private bool hasWalked;
        private bool canWalk;

        public bool CanWalk
        {
            get => canWalk;
            set
            {
                if (value)
                {
                    rigidbody2D.constraints = originalConstraints;
                    if (targetWalkVelocity > 0)
                        transform.rotation = Quaternion.Euler(0,  0, 0);
                    else if (targetWalkVelocity < 0)
                        transform.rotation = Quaternion.Euler(0,  180, 0);
                }
                else
                {
                    rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                canWalk = value;
            }
        }

        private bool ShouldWalk => targetWalkVelocity != 0;
        private bool IsGoingTooFast => Math.Abs(rigidbody2D.velocity.x) 
                                       > Math.Abs(targetWalkVelocity) + VelocityErrorMargin;
        private bool ChangedDirection => Math.Sign(rigidbody2D.velocity.x) != Math.Sign(targetWalkVelocity);

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            originalConstraints = rigidbody2D.constraints;
            canWalk = true;
        }
        
        private void FixedUpdate()
        {
            if (ShouldWalk && (!IsGoingTooFast || ChangedDirection))
                ApplyWalkVelocity();
            else
                hasWalked = false;

            ClampVelocity();
        }

        public void StartWalking(float value)
        {
            if (value == 0)
            {
                StopWalking();
                return;
            }

            if (CanWalk) transform.rotation = Quaternion.Euler(0, value > 0 ? 0 : 180, 0);
            targetWalkVelocity = value * walkSpeed;
            if (!IsGoingTooFast || ChangedDirection)
                ApplyWalkVelocity();
        }
        
        public void StopWalking()
        {
            targetWalkVelocity = 0;
            if (hasWalked)
                rigidbody2D.velocity = new Vector2(targetWalkVelocity, rigidbody2D.velocity.y);
        }
        
        private void ApplyWalkVelocity()
        {
            rigidbody2D.velocity = new Vector2(targetWalkVelocity, rigidbody2D.velocity.y);
            hasWalked = true;
        }
        
        private void ClampVelocity()
        {
            rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxVelocityMagnitude);
        }
    }
}