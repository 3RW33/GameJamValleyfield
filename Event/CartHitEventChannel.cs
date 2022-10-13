namespace Game
{
    public class CartHitEventChannel
    {
        public static event CartHitEvent OnCartHit;
        
        public static void Publish(int healthRemaining)
        {
            OnCartHit?.Invoke(healthRemaining);
        }
    }

    public delegate void CartHitEvent(int healthRemaining);
}