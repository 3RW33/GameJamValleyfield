namespace Game
{
    public static class MainMenuPageChangedEventChannel
    {
        public static event MainMenuPageChangedEvent OnPageChanged;

        public static void Publish(MainMenuPage newPage)
        {
            OnPageChanged?.Invoke(newPage);
        }
    }
    
    public delegate void MainMenuPageChangedEvent(MainMenuPage newPage);
}