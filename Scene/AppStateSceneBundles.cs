using Harmony;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "AppStateSceneBundles")]
    public class AppStateSceneBundles : ScriptableObject
    {
        public SceneBundle mainMenu;
        public SceneBundle game;
        public SceneBundle endGame;
    }
}