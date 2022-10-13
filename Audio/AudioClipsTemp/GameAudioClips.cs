using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "GameAudioClips")]
    public class GameAudioClips : ScriptableObject
    {
        public AudioClip pause;
        public AudioClip unpause;
        public AudioClip bowShot;
        public AudioClip mageDeath;
        public AudioClip torchmanDeath;
        public AudioClip enemyHit;
        public AudioClip cartHit;
        public AudioClip mageSpawn;
        public AudioClip swordSwing;
        public AudioClip switchToArcher;
        public AudioClip switchToSwordman;
        public AudioClip cartMovement;
        public AudioClip endScreenFire;
    }
}