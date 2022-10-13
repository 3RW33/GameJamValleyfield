using System.Collections;
using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.MainController)]
    public class SceneBundleManager : MonoBehaviour
    {
        private SceneBundleLoader loader;

        private void Awake()
        {
            loader = Finder.SceneBundleLoader;
        }

        public void Load(SceneBundle sceneBundle)
        {
            if (sceneBundle == null) return;
            
            loader.Load(sceneBundle);
        }

        public void Unload(SceneBundle sceneBundle)
        {
            if (sceneBundle == null) return;
            
            loader.Unload(sceneBundle);
        }

        public void Switch(SceneBundle oldSceneBundle, SceneBundle newSceneBundle)
        {
            if (oldSceneBundle == null)
                loader.Load(newSceneBundle);
            else if (newSceneBundle == null)
                loader.Unload(oldSceneBundle);
            else
                StartCoroutine(SwitchRoutine(oldSceneBundle, newSceneBundle));
        }

        // Replaces Harmony SceneBundleLoader.Switch() function that is broken in current version
        private IEnumerator SwitchRoutine(SceneBundle oldSceneBundle, SceneBundle newSceneBundle)
        {
            yield return loader.Unload(oldSceneBundle);

            yield return loader.Load(newSceneBundle);
        }
    }
}