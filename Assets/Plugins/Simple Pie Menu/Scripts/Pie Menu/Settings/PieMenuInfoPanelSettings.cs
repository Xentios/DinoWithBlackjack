using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuInfoPanelSettings : MonoBehaviour
    {
        public const float MaxInfoPanelScale = 2f;

        [OnValueChange(nameof(OnEnableValueChange))] [SerializeField]
        bool infoPanelEnabled;

        [OnValueChange(nameof(OnHeaderColorChange))] [SerializeField]
        Color headerColor;

        [OnValueChange(nameof(OnDetailsColorChange))] [SerializeField]
        Color detailsColor;

        [OnValueChange(nameof(OnScaleChange))] [Range(0f, MaxInfoPanelScale)] [SerializeField]
        float scale;

        private PieMenu pieMenu;
        private PieMenuInfoPanelSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeInfoPanelSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeInfoPanelSettings;
        }

        public void OnEnableValueChange()
        {
            settingsHandler.HandleEnableValueChange(pieMenu, infoPanelEnabled);
        }

        public void OnHeaderColorChange()
        {
            settingsHandler.ChangeHeaderColor(pieMenu, headerColor);
        }

        public void OnDetailsColorChange()
        {
            settingsHandler.ChangeDetailsColor(pieMenu, detailsColor);
        }

        public void OnScaleChange()
        {
            settingsHandler.ChangeScale(pieMenu, scale);
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
        }

        private void InitializeInfoPanelSettings()
        {
            settingsHandler = PieMenuShared.References.InfoPanelSettingsHandler;
            SetInspectorFields();

            pieMenu.PieMenuInfo.SetInfoPanelEnabled(infoPanelEnabled);
        }

        private void SetInspectorFields()
        {
            if (headerColor == null)
            {
                PieMenuElements pieMenuElements = pieMenu.PieMenuElements;
                headerColor = pieMenuElements.Header.color;
                detailsColor = pieMenuElements.Details.color;
                scale = pieMenuElements.InfoPanel.localScale.x;
            }
        }
    }
}
