namespace Game
{
    public static class AppStateChangedEventChannel
    {
        public static event AppStateChangedEvent OnAppStateChanged;

        public static void Publish(AppState oldState, AppState newState)
        {
            OnAppStateChanged?.Invoke(oldState, newState);
        }
    }
    
    public delegate void AppStateChangedEvent(AppState oldState, AppState newState);
}