using System;
using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.GameController)]
    public class Game : MonoBehaviour
    {
        [SerializeField] private float environmentMovementSpeed = 1;
        
        private Inputs inputs;
        private new Audio audio;

        public float EnvironmentMovementSpeed => environmentMovementSpeed;

        private void Awake()
        {
            inputs = Finder.Inputs;
            audio = Finder.Audio;
        }

        private void Start()
        {
            EnableInitialActionMaps();
            audio.MainMusicPlayer.Play(audio.MusicClips.game);
            audio.AmbientSoundPlayer.Play(audio.GameClips.cartMovement);
        }

        private void OnDisable()
        {
            audio.MainMusicPlayer.Stop();
            audio.AmbientSoundPlayer.Stop();
        }

        private void OnDestroy()
        {
            DisableInitialActionMaps();
        }

        private void EnableInitialActionMaps()
        {
            inputs.Actions.Game.Enable();
            inputs.Actions.Pause.Enable();
            inputs.Actions.Fighter.Enable();
            inputs.Actions.Archer.Enable();
        }

        private void DisableInitialActionMaps()
        {
            inputs.Actions.Game.Enable();
            inputs.Actions.Pause.Enable();
            inputs.Actions.Fighter.Enable();
            inputs.Actions.Archer.Enable();
        }
    }
}