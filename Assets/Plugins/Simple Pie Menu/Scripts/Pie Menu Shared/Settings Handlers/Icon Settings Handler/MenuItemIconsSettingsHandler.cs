using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(IconEnableHandler))]
    [RequireComponent(typeof(IconOffsetChangeHandler))]
    [RequireComponent(typeof(IconScaleChangeHandler))]
    [RequireComponent(typeof(IconGetter))]
    [ExecuteInEditMode]
    public class MenuItemIconsSettingsHandler : MonoBehaviour
    {
        public IconEnableHandler EnableHandler { get; private set; }
        public IconOffsetChangeHandler OffsetHandler { get; private set; }
        public IconScaleChangeHandler ScaleHandler { get; private set; }
        public IconGetter IconGetter { get; private set; }

        private void OnEnable()
        {
            EnableHandler = GetComponent<IconEnableHandler>();
            OffsetHandler = GetComponent<IconOffsetChangeHandler>();
            ScaleHandler = GetComponent<IconScaleChangeHandler>();
            IconGetter = GetComponent<IconGetter>();
        }

        public void HandleIconEnableSettingChange(PieMenu pieMenu, bool addIcons)
        {
            EnableHandler.Handle(pieMenu, addIcons);
        }

        public void HandleIconOffsetChange(PieMenu pieMenu, int offsetFromCenter)
        {
            OffsetHandler.Handle(pieMenu, IconGetter, offsetFromCenter);
        }

        public void HandleIconScaleChange(PieMenu pieMenu, float iconScale)
        {
            ScaleHandler.Handle(pieMenu, IconGetter, iconScale);
        }
    }
}
