using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.MainController)]
    public class AppStateSceneManager : MonoBehaviour
    {
        [SerializeField] private AppStateSceneBundles sceneBundles;
        [SerializeField] private bool loadInitialState = true;

        private Main main;
        private SceneBundleManager sceneBundleManager;

        public bool LoadInitialState
        {
            get => loadInitialState;
            set => loadInitialState = value;
        }

        private void Awake()
        {
            main = Finder.Main;
            sceneBundleManager = Finder.SceneBundleManager;
        }

        private void OnEnable()
        {
            AppStateChangedEventChannel.OnAppStateChanged += OnAppStateChanged;
        }

        private void Start()
        {
            if (LoadInitialState) sceneBundleManager.Load(GetAppStateBundle(main.CurrentState));
        }

        private void OnDisable()
        {
            AppStateChangedEventChannel.OnAppStateChanged -= OnAppStateChanged;
        }

        private void OnAppStateChanged(AppState oldState, AppState newState)
        {
            sceneBundleManager.Switch(GetAppStateBundle(oldState), GetAppStateBundle(newState));
        }

        private SceneBundle GetAppStateBundle(AppState state)
        {
            return state switch
            {
                AppState.MainMenu => sceneBundles.mainMenu,
                AppState.Game => sceneBundles.game,
                AppState.EndGame => sceneBundles.endGame,
                _ => null
            };
        }
    }
}