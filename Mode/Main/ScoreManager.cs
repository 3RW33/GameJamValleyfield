using System.Collections;
using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.MainController)]
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private float delayForPointIncrease = 1f;

        private Coroutine currentPointCoroutine;
        private Main main;
        
        public int Points { get; private set; }

        private void Awake()
        {
            main = Finder.Main;
            Points = 0;
        }

        private void Start()
        {
            if (main.CurrentState == AppState.Game)
                currentPointCoroutine = StartCoroutine(GivePoints());
        }

        private void OnEnable()
        {
            AppStateChangedEventChannel.OnAppStateChanged += OnAppStateChange;
        }

        private void OnDisable()
        {
            AppStateChangedEventChannel.OnAppStateChanged -= OnAppStateChange;
        }

        private void OnAppStateChange(AppState oldState, AppState newState)
        {
            if (newState == AppState.Game)
            {
                currentPointCoroutine = StartCoroutine(GivePoints());
            }
            else if (newState == AppState.EndGame)
            {
                StopCoroutine(currentPointCoroutine);
            }
        }

        private IEnumerator GivePoints()
        {
            while (true)
            {
                Points++;
                yield return new WaitForSeconds(delayForPointIncrease);
            }
        }
    }
}