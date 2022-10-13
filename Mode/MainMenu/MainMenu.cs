using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [Findable(Tags.MainMenuController)]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuPage currentPage;
        
        private Main main;
        private Inputs inputs;
        private new Audio audio;

            public MainMenuPage CurrentPage
        {
            get => currentPage;
            private set
            {
                if (currentPage == value) return;

                currentPage = value;
                MainMenuPageChangedEventChannel.Publish(value);
            }
        }

        private void Awake()
        {
            main = Finder.Main;
            inputs = Finder.Inputs;
            audio = Finder.Audio;
        }

        private void OnEnable()
        {
            inputs.Actions.UI.Navigate.started += OnNavigate;
            inputs.Actions.UI.Submit.started += OnSubmit;
            inputs.Actions.UI.Cancel.started += OnCancel;
        }

        private void Start()
        {
            EnableInitialActionMaps();
            audio.MainMusicPlayer.Play(audio.MusicClips.mainMenu);
        }

        private void OnDisable()
        {
            inputs.Actions.UI.Navigate.started -= OnNavigate;
            inputs.Actions.UI.Submit.started -= OnSubmit;
            inputs.Actions.UI.Cancel.started -= OnCancel;
        }

        private void OnDestroy()
        {
            audio.MainMusicPlayer.Stop();
            DisableInitialActionMaps();
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            audio.MainSoundPlayer.Play(audio.UIClips.navigate);
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            audio.MainSoundPlayer.Play(audio.UIClips.submit);
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            audio.MainSoundPlayer.Play(audio.UIClips.cancel);
            GoToPreviousPage();
        }
        
        private void EnableInitialActionMaps()
        {
            inputs.Actions.UI.Enable();
        }

        private void DisableInitialActionMaps()
        {
            inputs.Actions.UI.Enable();
        }

        public void ChangePage(MainMenuPage page)
        {
            if (page != null) CurrentPage = page;
        }

        public void GoToPreviousPage()
        {
            ChangePage(CurrentPage.PreviousPage);
        }

        public void GoToGame()
        {
            main.GoToGame();
        }

        public void Quit()
        {
            main.Quit();
        }
    }
}