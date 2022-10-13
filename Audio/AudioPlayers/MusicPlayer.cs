using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : BaseAudioPlayer
    {
        public override void Play(AudioClip clip)
        {
            if (clip == null) return;
            
            Source.clip = clip;
            Source.Play();
        }
    }
}