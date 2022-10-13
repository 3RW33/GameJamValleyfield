using Harmony;
using UnityEngine;

namespace Game
{
    public abstract class ControllableCharacter : Character
    {
        [SerializeField] private SpriteRenderer selectionArrowSpriteRenderer;
        
        protected Inputs Inputs { get; private set; }

        public bool IsBeingControlled { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Inputs = Finder.Inputs;

            selectionArrowSpriteRenderer.enabled = false;
        }

        private void OnDestroy()
        {
            if (IsBeingControlled)
                StopControlling();
        }

        public virtual void StartControlling()
        {
            selectionArrowSpriteRenderer.enabled = true;
            IsBeingControlled = true;
            
        }

        public virtual void StopControlling()
        {
            selectionArrowSpriteRenderer.enabled = false;
            IsBeingControlled = false;
        }
    }
}