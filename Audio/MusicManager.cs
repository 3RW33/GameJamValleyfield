using Harmony;
using UnityEngine;

namespace Game
{
    public class MusicManager : MonoBehaviour
    {
        private new Audio audio;
        private Main main;

        private MusicPlayer Player => audio.MainMusicPlayer;

        private void Awake()
        {
            audio = Finder.Audio;
            main = Finder.Main;
        }

        private void OnEnable()
        {
            AppStateChangedEventChannel.OnAppStateChanged += OnAppStateChanged;
        }

        private void Start()
        {
            Play(GetAppStateClip(main.CurrentState));
        }

        private void OnDisable()
        {
            AppStateChangedEventChannel.OnAppStateChanged -= OnAppStateChanged;

        }

        private void OnAppStateChanged(AppState oldState, AppState newState)
        {
            Play(GetAppStateClip(newState));
        }

        private void Play(AudioClip clip)
        {
            Player.Play(clip);
        }

        private AudioClip GetAppStateClip(AppState state)
        {
            return state switch
            {
                AppState.MainMenu => audio.MusicClips.mainMenu,
                AppState.Game => audio.MusicClips.game,
                AppState.EndGame => audio.MusicClips.endGame,
                _ => null
            };
        }
    }
}