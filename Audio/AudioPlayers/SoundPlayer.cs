using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : BaseAudioPlayer
    {
        public override void Play(AudioClip clip)
        {
            if (clip == null) return;

            Source.PlayOneShot(clip);
        }
    }
}