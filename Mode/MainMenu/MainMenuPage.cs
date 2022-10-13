using System;
using Harmony;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    public class MainMenuPage : MonoBehaviour
    {
        //Drag the object which you want to be automatically selected by the keyboard or gamepad when this panel becomes active
        [SerializeField] private GameObject firstSelectedObject;
        [SerializeField] private MainMenuPage previousPage;

        private MainMenu mainMenu;
        private EventSystem eventSystem;
        private Inputs inputs;
        
        public MainMenuPage PreviousPage => previousPage;
        private bool ShouldBeActive => mainMenu.CurrentPage == this;

        private void Awake()
        {
            mainMenu = Finder.MainMenu;
            eventSystem = EventSystem.current;
            inputs = Finder.Inputs;
            
            MainMenuPageChangedEventChannel.OnPageChanged += OnPageChanged;
        }

        private void OnEnable()
        {
            inputs.Actions.UI.Navigate.started += OnNavigate;
            inputs.Actions.UI.Navigate.started += OnSubmit;
            inputs.Actions.UI.Navigate.started += OnCancel;
        }

        private void Start()
        {
            UpdateVisibility();
        }
        
        private void OnDisable()
        {
            inputs.Actions.UI.Navigate.started -= OnNavigate;
            inputs.Actions.UI.Navigate.started -= OnSubmit;
            inputs.Actions.UI.Navigate.started -= OnCancel;
            
        }

        private void OnDestroy()
        {
            MainMenuPageChangedEventChannel.OnPageChanged -= OnPageChanged;
        }

        private void OnPageChanged(MainMenuPage newPage)
        {
            UpdateVisibility();
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            UseNavigation();
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            UseNavigation();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            UseNavigation();
        }

        private void UseNavigation()
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                SetFirstSelected();
            }
        }

        private void UpdateVisibility()
        {
            gameObject.SetActive(ShouldBeActive);
            if (ShouldBeActive) SetFirstSelected();
        }

        private void SetFirstSelected()
        {
            //Tell the EventSystem to select this object
            eventSystem.SetSelectedGameObject(firstSelectedObject);
        }
    }
}