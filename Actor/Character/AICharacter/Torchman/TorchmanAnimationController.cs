using UnityEngine;

namespace Game
{
    public class TorchmanAnimationController : MonoBehaviour
    {
        private Torchman torchman;

        private void Awake()
        {
            torchman = GetComponentInParent<Torchman>();
        }

        private void OnAttackOver()
        {
            torchman.OnAttackAnimationOver();
        }
    }
}