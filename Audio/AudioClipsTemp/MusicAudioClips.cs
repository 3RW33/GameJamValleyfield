using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "MusicAudioClips")]
    public class MusicAudioClips : ScriptableObject
    {
        public AudioClip mainMenu;
        public AudioClip game;
        public AudioClip endGame;
    }
}