using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuDrawer : MonoBehaviour
    {
        PieMenuGeneralSettingsHandler settingsHandler;
        PieMenuInfo pieMenuInfo;
        int menuItemCount;
        int menuItemSpacing;

        public void Redraw(PieMenu pieMenu)
        {
            Transform menuItemsDir = pieMenu.PieMenuElements.MenuItemsDir;

            settingsHandler = PieMenuShared.References.GeneralSettingsHandler;
            pieMenuInfo = pieMenu.PieMenuInfo;
            menuItemCount = menuItemsDir.childCount;
            menuItemSpacing = pieMenuInfo.MenuItemSpacing;
            int rotation = pieMenuInfo.Rotation;

            settingsHandler.HandleRotationChange(pieMenu, 0);

            pieMenu.MenuItemsTracker.Initialize(menuItemsDir);

            settingsHandler.UpdateButtons(pieMenu, menuItemCount, menuItemSpacing);

            ManageMenuItemSpacing(pieMenu);
            settingsHandler.HandleRotationChange(pieMenu, rotation);
        }

        private void ManageMenuItemSpacing(PieMenu pieMenu)
        {
            int preservedSpacing = pieMenuInfo.MenuItemPreservedSpacing;
            int newSpacing;
            if (menuItemCount == 1)
            {
                pieMenuInfo.SetPreservedSpacing(menuItemSpacing);
                newSpacing = 0;
                settingsHandler.HandleButtonSpacingChange(pieMenu, menuItemCount, newSpacing);
            }
            else if (preservedSpacing != -1)
            {
                newSpacing = preservedSpacing;
                pieMenuInfo.SetPreservedSpacing(-1);
                settingsHandler.HandleButtonSpacingChange(pieMenu, menuItemCount, newSpacing);
            }
        }
    }
}
