using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    public class MenuItemDisabler : MonoBehaviour
    {
        public void ToggleDisable(PieMenu pieMenu, int menuItemId, bool disabled)
        {
            Button button = pieMenu.MenuItemsTracker.ButtonComponents[menuItemId];
            button.interactable = !disabled;

            ChangeIconAppearance(pieMenu, button.transform, disabled);
        }

        private void ChangeIconAppearance(PieMenu pieMenu, Transform menuItem, bool disabled)
        {
            bool iconsEnabled = pieMenu.PieMenuInfo.IconsEnabled;

            if (!iconsEnabled) return;
            else
            {
                IconGetter iconGetter = PieMenuShared.References.IconsSettingsHandler.IconGetter;
                Image icon = iconGetter.GetIcon(menuItem).GetComponent<Image>();
                float transparency;

                if (disabled && iconsEnabled)
                {
                    transparency = 0.80f;
                    ModifyIconTransparency(icon, transparency);
                }
                else if (!disabled && iconsEnabled)
                {
                    transparency = 1f;
                    ModifyIconTransparency(icon, transparency);
                }
            }
        }

        private void ModifyIconTransparency(Image icon, float transparencyValue)
        {
            Color color = icon.color;
            color.a = transparencyValue;
            icon.color = color;
        }
    }
}
