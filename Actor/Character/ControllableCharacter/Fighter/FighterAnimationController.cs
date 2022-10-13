using UnityEngine;

namespace Game
{
    public class FighterAnimationController : MonoBehaviour
    {
        private Fighter fighter;
        
        private void Awake()
        {
            fighter = GetComponentInParent<Fighter>();
        }

        public void OnAttackOver()
        {
            fighter.OnAttackAnimationOver();
        }
    }
}