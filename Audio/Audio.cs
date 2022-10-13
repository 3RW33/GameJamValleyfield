using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.AudioController)]
    public class Audio : MonoBehaviour
    {
        [Header("Audio clips")]
        [SerializeField] private MusicAudioClips musicAudioClips;
        [SerializeField] private UIAudioClips uiAudioClips;
        [SerializeField] private GameAudioClips gameAudioClips;

        [Header("Main audio players")]
        [SerializeField] private MusicPlayer mainMusicPlayer;
        [SerializeField] private SoundPlayer mainSoundPlayer;
        [SerializeField] private MusicPlayer ambientSoundPlayer;

        public MusicAudioClips MusicClips => musicAudioClips;
        public UIAudioClips UIClips => uiAudioClips;
        public GameAudioClips GameClips => gameAudioClips;

        public MusicPlayer MainMusicPlayer => mainMusicPlayer;
        public SoundPlayer MainSoundPlayer => mainSoundPlayer;

        public MusicPlayer AmbientSoundPlayer => ambientSoundPlayer;
    }
}