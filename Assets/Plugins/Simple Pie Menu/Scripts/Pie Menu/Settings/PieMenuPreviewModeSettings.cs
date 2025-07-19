using UnityEngine;


namespace SimplePieMenu
{
    public class PieMenuPreviewModeSettings : MonoBehaviour
    {
        [Tooltip("When enabled, your menu will automatically reopen after it has been closed. " +
            "The purpose of this option is to assist in testing animations and sounds during the configuration of your menu.")]
        [OnValueChange(nameof(OnEnableValueChange))]
        [SerializeField] bool previewModeEnabled;

        private PieMenu pieMenu;
        private PieMenuPreviewModeSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializePreviewModeSettings;
            pieMenu.OnPieMenuFullyInitialized += EnablePreviewMode;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializePreviewModeSettings;
            pieMenu.OnPieMenuFullyInitialized -= EnablePreviewMode;
        }

        public void OnEnableValueChange()
        {
            if (Application.isPlaying)
            {
                if (!pieMenu.PieMenuInfo.IsActive)
                    PieMenuShared.References.PieMenuToggler.SetActive(pieMenu, true);

                settingsHandler.HandleEnableValueChange(pieMenu, previewModeEnabled);
            }
        }

        private void InitializePreviewModeSettings()
        {
            settingsHandler = PieMenuShared.References.PreviewModeSettingsHandler;
        }

        private void EnablePreviewMode()
        {
            if (previewModeEnabled)
            {
                settingsHandler.HandleEnableValueChange(pieMenu, previewModeEnabled);
            }
        }
    }
}

