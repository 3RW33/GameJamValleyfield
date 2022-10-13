using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "UIAudioClips")]
    public class UIAudioClips : ScriptableObject
    {
        public AudioClip navigate;
        public AudioClip submit;
        public AudioClip cancel;
    }
}