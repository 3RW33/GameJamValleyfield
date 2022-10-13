using Harmony;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class EditorPreloadedSceneManager : MonoBehaviour
    {
#if UNITY_EDITOR
        // Check if a scene other than main is loaded in the editor
        private static bool ContentScenesAlreadyLoaded => SceneManager.sceneCount > Main.MainScenesCount;

        private void Start()
        {
            if (ContentScenesAlreadyLoaded)
            {
                Finder.Main.UpdateCurrentAppState();
                Finder.AppStateSceneManager.LoadInitialState = false;
            }
        }
#endif
    }
}