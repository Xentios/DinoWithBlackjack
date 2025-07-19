using UnityEngine;

namespace SimplePieMenu
{
    public class MenuItemsRemover : MonoBehaviour
    {
        public void Remove(PieMenu pieMenu, int menuItemsCount)
        {
            int currentMenuItemsCount = pieMenu.MenuItemsTracker.PieMenuItems.Count;
            int itemsToRemove = currentMenuItemsCount - menuItemsCount;

            int lastItemIndex = currentMenuItemsCount - 1;
            for (int i = 0; i < itemsToRemove; i++)
            {
                pieMenu.MenuItemsTracker.RemoveMenuItem(lastItemIndex);
                lastItemIndex--;
            }
        }
    }
}
