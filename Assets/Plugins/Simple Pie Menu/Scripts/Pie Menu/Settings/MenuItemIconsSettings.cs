using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemIconsSettings : MonoBehaviour
    {
        [OnValueChange(nameof(OnEnableSettingChange))] [SerializeField]
        bool addIcons;

        [OnValueChange(nameof(OnMoveValueChange))] [Range(-500f, 0f)] [SerializeField]
        int offsetFromCenter;

        [OnValueChange(nameof(OnScaleValueChange))] [Range(0f, 3f)] [SerializeField]
        float iconScale;

        private PieMenu pieMenu;
        private MenuItemIconsSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeIconsSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeIconsSettings;
        }

        public void OnEnableSettingChange()
        {
            settingsHandler.HandleIconEnableSettingChange(pieMenu, addIcons);

            if (!addIcons)
            {
                offsetFromCenter = 0;
                iconScale = 0f;
            }
        }

        public void OnMoveValueChange()
        {
            if (!addIcons) return;
            settingsHandler.HandleIconOffsetChange(pieMenu, offsetFromCenter);
        }

        public void OnScaleValueChange()
        {
            if (!addIcons) return;
            settingsHandler.HandleIconScaleChange(pieMenu, iconScale);
        }

        public void SetScale(float newScale)
        {
            iconScale = newScale;
        }

        public void SetOffset(int newOffset)
        {
            offsetFromCenter = newOffset;
        }

        private void InitializeIconsSettings()
        {
            settingsHandler = PieMenuShared.References.IconsSettingsHandler;

            pieMenu.PieMenuInfo.SetIconsEnabled(addIcons);
        }
    }
}