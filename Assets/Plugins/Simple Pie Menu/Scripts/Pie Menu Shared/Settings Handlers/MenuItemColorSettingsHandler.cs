using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemColorSettingsHandler : MonoBehaviour
    {
        public void HandleColorChange(PieMenu pieMenu, ColorBlock newColors)
        {
            ChangeMenuItemsColors(pieMenu, newColors);
        }

        private void ChangeMenuItemsColors(PieMenu pieMenu, ColorBlock newColors)
        {
            foreach (KeyValuePair<int, Button> button in pieMenu.MenuItemsTracker.ButtonComponents)
            {
                Change(button.Value, newColors);
            }
        }

        private void Change(Button button, ColorBlock newColors)
        {
            ColorBlock colors = button.colors;

            colors.normalColor = newColors.normalColor;
            colors.highlightedColor = newColors.highlightedColor;
            colors.selectedColor = newColors.selectedColor;
            colors.disabledColor = newColors.disabledColor;

            button.colors = colors;
        }
    }
}