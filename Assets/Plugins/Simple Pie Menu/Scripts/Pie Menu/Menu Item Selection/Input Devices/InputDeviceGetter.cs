using System;
using UnityEngine;

namespace SimplePieMenu
{
    public class InputDeviceGetter : MonoBehaviour
    {
        public IInputDevice InputDevice;
        private bool isOldInputSystemEnabled;
        private bool isNewInputSystemEnabled;

        private void Awake()
        {
            DetectInputSystem();

            HandleInputDevicePreferences();
        }

        private void DetectInputSystem()
        {
#if ENABLE_INPUT_SYSTEM
            // New input system backends are enabled.
            isNewInputSystemEnabled = true;
#endif

#if ENABLE_LEGACY_INPUT_MANAGER
            // Old input backends are enabled.
            isOldInputSystemEnabled = true;
#endif
        }

        private void HandleInputDevicePreferences()
        {
            int defaultValue = -1;
            int inputDeviceId = PlayerPrefs.GetInt(PieMenuPlayerPrefs.InputDevice, defaultValue);

            if (isOldInputSystemEnabled)
            {
                if (inputDeviceId == (int)AvailableInputDevices.MouseAndKeyboard_OLD_INPUT_SYSTEM)
                    SetInputDevice<MouseAndKeyboard_OLD_INPUT_SYSTEM>();
                else SetDefault();
            }
            else if (isNewInputSystemEnabled)
            {
                if (inputDeviceId == (int)AvailableInputDevices.MouseAndKeyboard_NEW_INPUT_SYSTEM)
                    SetInputDevice<MouseAndKeyboard_NEW_INPUT_SYSTEM>();
                else SetDefault();
            }
        }

        private void SetDefault()
        {
            if (isOldInputSystemEnabled)
            {
                SetInputDevice<MouseAndKeyboard_OLD_INPUT_SYSTEM>();
            }
            else if (isNewInputSystemEnabled)
            {
                SetInputDevice<MouseAndKeyboard_NEW_INPUT_SYSTEM>();
            }
        }

        private void SetInputDevice<T>() where T : MonoBehaviour, IInputDevice
        {
            if (typeof(IInputDevice).IsAssignableFrom(typeof(T)))
            {
                InputDevice = gameObject.AddComponent<T>();
            }
            else
            {
                throw new InvalidOperationException($"Type {typeof(T)} must derive from MonoBehaviour and implement IInputDevice interface.");
            }
        }
    } 
}
