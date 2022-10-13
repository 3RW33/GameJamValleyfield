using System;
using Harmony;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [Findable(Tags.EndGameController)]
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text textBox;
        
        private ScoreManager scoreManager;
        private Inputs inputs;
        private Main main;
        private new Audio audio;

        private void Awake()
        {
            scoreManager = Finder.ScoreManager;
            inputs = Finder.Inputs;
            main = Finder.Main;
            audio = Finder.Audio;

            textBox.text = String.Format("Score: " + scoreManager.Points);
        }

        private void OnEnable()
        {
            audio.MainSoundPlayer.Play(audio.MusicClips.endGame);
            audio.AmbientSoundPlayer.Play(audio.GameClips.endScreenFire);
            inputs.Actions.UI.Submit.started += OnEnter;
        }

        private void Start()
        {
            EnableInitialActionMaps();
        }

        private void OnDisable()
        {
            audio.MainSoundPlayer.Stop();
            audio.AmbientSoundPlayer.Stop();
            inputs.Actions.UI.Submit.started -= OnEnter;
        }

        private void OnDestroy()
        {
            DisableInitialActionMaps();
        }

        private void OnEnter(InputAction.CallbackContext context)
        {
            main.GoToMenu();
        }

        private void EnableInitialActionMaps()
        {
            inputs.Actions.UI.Enable();
        }

        private void DisableInitialActionMaps()
        {
            inputs.Actions.UI.Disable();
        }
    }
}