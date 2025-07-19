using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuInputSettings : MonoBehaviour
    {
        [OnValueChange(nameof(OnInputDeviceChange))] [SerializeField]
        AvailableInputDevices inputDevice;

        public void OnInputDeviceChange()
        {
            PlayerPrefs.SetInt(PieMenuPlayerPrefs.InputDevice, (int)inputDevice);
        }
    }

    public enum AvailableInputDevices
    {
        MouseAndKeyboard_OLD_INPUT_SYSTEM = 0,
        MouseAndKeyboard_NEW_INPUT_SYSTEM = 1,
    }
}
