using UnityEngine;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        public bool IsFriendly { get; protected set; }
        
        public int Damage => damage;
    }
}