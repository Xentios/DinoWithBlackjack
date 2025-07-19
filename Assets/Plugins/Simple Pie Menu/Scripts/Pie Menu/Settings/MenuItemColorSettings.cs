using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemColorSettings : MonoBehaviour
    {
        [OnValueChange(nameof(OnColorValueChange))] [SerializeField]
        Color normalColor;

        [OnValueChange(nameof(OnColorValueChange))] [SerializeField]
        Color selectedColor;

        [OnValueChange(nameof(OnColorValueChange))] [SerializeField]
        Color disabledColor;

        private PieMenu pieMenu;
        private MenuItemColorSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeMenuItemColorSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeMenuItemColorSettings;
        }

        public void OnColorValueChange()
        {
            ColorBlock colors = new();
            colors.normalColor = normalColor;
            colors.highlightedColor = normalColor;
            colors.selectedColor = selectedColor;
            colors.disabledColor = disabledColor;

            settingsHandler.HandleColorChange(pieMenu, colors);
        }

        private void InitializeMenuItemColorSettings()
        {
            settingsHandler = PieMenuShared.References.MenuItemColorSettingsHandler;
            SetColorFields();
        }

        private void SetColorFields()
        {
            Transform menuItem = pieMenu.MenuItemTemplate.GetTemplate(pieMenu.GetMenuItems()).transform;
            ColorBlock colors = menuItem.GetComponent<Button>().colors;

            normalColor = colors.normalColor;
            selectedColor = colors.selectedColor;
            disabledColor = colors.disabledColor;
        }
    }
}
