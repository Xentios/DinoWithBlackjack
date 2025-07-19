using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuBackgroundSettings : MonoBehaviour
    {
        [OnValueChange(nameof(OnEnableValueChange))] [SerializeField]
        bool backgroundEnabled;

        [OnValueChange(nameof(OnColorChange))] [SerializeField]
        Color backgroundColor;

        private PieMenu pieMenu;
        private PieMenuBackgroundSettingsHandler settingsHandler;
        private Image background;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeBackgroundSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeBackgroundSettings;
        }

        public void OnEnableValueChange()
        {
            settingsHandler.SetActive(background, backgroundEnabled);
        }

        public void OnColorChange()
        {
            settingsHandler.ChangeColor(background, backgroundColor);
        }

        private void InitializeBackgroundSettings()
        {
            settingsHandler = PieMenuShared.References.BackgroundSettingsHandler;
            background = pieMenu.PieMenuElements.Background;
            pieMenu.PieMenuInfo.SetBackgroundEnabled(backgroundEnabled);
        }
    }
}
