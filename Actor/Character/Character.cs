using UnityEngine;

namespace Game
{
    public class Character : MonoBehaviour
    {
        protected CharacterMover Mover { get; private set; }

        protected virtual void Awake()
        {
            Mover = GetComponent<CharacterMover>();
        }
    }
}