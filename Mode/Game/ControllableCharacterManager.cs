using System;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class ControllableCharacterManager : MonoBehaviour
    {
        [SerializeField] private ControllableCharacter initialCharacter;
        [SerializeField] private ControllableCharacter[] characters;

        private int activeCharacterIndex;

        private InputActions.GameActions gameInputs;
        
        private void Awake()
        {
            gameInputs = Finder.Inputs.Actions.Game;
            if (FindCharacterIndex(initialCharacter, out var index))
            {
                activeCharacterIndex = index;
            }
            else
            {
                activeCharacterIndex = 0;
                initialCharacter = null;
            }
        }

        private void OnEnable()
        {
            gameInputs.SwitchCharacter.started += OnSwitchCharacter;
        }

        private void Start()
        {
            characters[activeCharacterIndex].StartControlling();
        }

        private void OnDisable()
        {
            gameInputs.SwitchCharacter.started -= OnSwitchCharacter;
        }

        private bool FindCharacterIndex(ControllableCharacter characterToFind, out int index)
        {
            index = -1;
            if (characterToFind == null) return false;

            index = Array.IndexOf(characters, characterToFind);
            return index != -1;
        }

        private void OnSwitchCharacter(InputAction.CallbackContext context)
        {
            var newIndex = (activeCharacterIndex + 1) % characters.Length;
            ChangeActiveCharacter(newIndex);
        }

        private void ChangeActiveCharacter(int newCharacterIndex)
        {
            characters[activeCharacterIndex].StopControlling();
            characters[newCharacterIndex].StartControlling();
            activeCharacterIndex = newCharacterIndex;
        }
    }
}