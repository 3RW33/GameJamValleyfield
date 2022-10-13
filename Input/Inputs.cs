using System.Collections.Generic;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Game
{
    [Findable(Tags.MainController)]
    public class Inputs : MonoBehaviour
    {
        private InputActions actions;
        private Main main;

        public InputActions Actions => actions ??= new InputActions();
        public InputControlScheme CurrentControlScheme { get; private set; }

        private void Awake()
        {
            main = Finder.Main;
            
            CurrentControlScheme = Actions.KeyboardMouseScheme;
            Actions.Disable();
        }

        private void OnEnable()
        {
            InputUser.onChange += OnInputDeviceChanged;
        }

        private void OnDisable()
        {
            InputUser.onChange -= OnInputDeviceChanged;
        }

        private void OnInputDeviceChanged(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                if (user.controlScheme != null) CurrentControlScheme = user.controlScheme.Value;
                Cursor.visible = CurrentControlScheme != Actions.GamepadScheme;
                
                ControlSchemeChangedEventChannel.Publish(CurrentControlScheme);
            }

            if (change == InputUserChange.DeviceLost)
            {
                CurrentControlScheme = Actions.KeyboardMouseScheme;
                ControlSchemeChangedEventChannel.Publish(CurrentControlScheme);
            }
        }

        private void EnableActionMaps(List<InputActionMap> actionMaps)
        {
            actionMaps.ForEach(map => map.Enable());
        }

        private void DisableActionMaps(List<InputActionMap> actionMaps)
        {
            actionMaps.ForEach(map => map.Disable());
        }
    }
}