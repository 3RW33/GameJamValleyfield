using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.MainController)]
    public class Main : MonoBehaviour
    {
        [SerializeField] private AppState currentState;

        public const int MainScenesCount = 1;

        public AppState CurrentState
        {
            get => currentState;
            private set
            {
                if (currentState == value
                    // Don't change state if the currrent state is invalid, because scenes won't unload properly
                    || currentState == AppState.Invalid) return;

                var oldState = currentState;
                currentState = value;
                AppStateChangedEventChannel.Publish(oldState, value);
            }
        }

        public void GoToMenu()
        {
            CurrentState = AppState.MainMenu;
        }

        public void GoToGame()
        {
            CurrentState = AppState.Game;
        }

        public void GoToEndGame()
        {
            CurrentState = AppState.EndGame;
        }

        public void Quit()
        {
            //If we are running in a standalone build of the game
#if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

#if UNITY_EDITOR
        public void UpdateCurrentAppState()
        {
            var statesFound = new List<AppState>();

            if (Finder.Game)
                statesFound.Add(AppState.Game);
            if (Finder.MainMenu)
                statesFound.Add(AppState.MainMenu);
            if (Finder.EndGame)
                statesFound.Add(AppState.EndGame);

            currentState = statesFound.Count == 1 ? statesFound[0] : AppState.Invalid;
        }
#endif
    }
}