using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemsCreator : MonoBehaviour
    {
        public void Create(PieMenu pieMenu, int newMenuItemsCount)
        {
            Dictionary<int, PieMenuItem> menuItems = pieMenu.MenuItemsTracker.PieMenuItems;

            int menuItemsToCreate = newMenuItemsCount - menuItems.Count;

            GameObject template = pieMenu.MenuItemTemplate.GetTemplate(menuItems);
            Transform menuItemsDir = pieMenu.PieMenuElements.MenuItemsDir;

            for (int i = 0; i < menuItemsToCreate; i++)
            {
                GameObject newMenuItem = Instantiate(template, menuItemsDir);

                int menuItemIndex = menuItems.Count;
                pieMenu.MenuItemsTracker.InitializeMenuItem(newMenuItem.transform, menuItemIndex);
            }
        }
    }
}
