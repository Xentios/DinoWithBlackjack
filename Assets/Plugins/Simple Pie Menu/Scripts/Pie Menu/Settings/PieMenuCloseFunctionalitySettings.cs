using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuCloseFunctionalitySettings : MonoBehaviour
    {
        [Tooltip("This setting will allow the player to close the menu by clicking the 'Close' button. " +
                 "By default, this is set to 'Esc' for mouse input devices. For more information, please refer to the documentation.")]
        [OnValueChange(nameof(OnCloseableValueChange))]
        [SerializeField]
        bool closeable;

        private PieMenu pieMenu;
        private PieMenuCloseFunctionalitySettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += OnCloseSettingsInitialized;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= OnCloseSettingsInitialized;
        }

        public void OnCloseableValueChange()
        {
            if (Application.isPlaying)
                settingsHandler.Handle(pieMenu, closeable);

        }

        private void OnCloseSettingsInitialized()
        {
            settingsHandler = PieMenuShared.References.CloseSettingsHandler;
            OnCloseableValueChange();
        }
    }
}
