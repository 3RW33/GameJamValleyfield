using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        protected AudioSource Source
        {
            get => audioSource;
            private set => audioSource = value;
        }

        private void Awake()
        {
            Source ??= gameObject.GetComponent<AudioSource>();
        }

        public void Stop()
        {
            if (Source != null) Source.Stop();
        }

        public abstract void Play(AudioClip clip);
    }
}