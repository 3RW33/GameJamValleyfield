using UnityEngine.InputSystem;

namespace Game
{
    public static class ControlSchemeChangedEventChannel
    {
        public static event ControlSchemeChangedEvent OnControlSchemeChanged;

        public static void Publish(InputControlScheme controlScheme)
        {
            OnControlSchemeChanged?.Invoke(controlScheme);
        }
    }
    
    public delegate void ControlSchemeChangedEvent(InputControlScheme controlScheme);
}