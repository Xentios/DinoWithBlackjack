using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    public class MenuItemRestorer : MonoBehaviour
    {
        private readonly Dictionary<int, PieMenuItem> restoredMenuItems = new();
        private PieMenuElements pieMenuElements;
        private MenuItemsTracker tracker;

        Dictionary<int, PieMenuItem> menuItems;

        public void RestoreMenuItems(PieMenu pieMenu, List<int> menuItemIds = null)
        {
            restoredMenuItems.Clear();
            GetComponents(pieMenu);
            MoveMenuItems(menuItems, pieMenuElements.HiddenMenuItemsDir);

            Dictionary<int, PieMenuItem> hiddenMenuItems = tracker.HiddenMenuItems;
            int menuItemsCount = menuItems.Count + hiddenMenuItems.Count;

            if (menuItemIds == null)
            {
                RestoreAllHiddenMenuItems(hiddenMenuItems, menuItemsCount);
            }
            else
            {
                RestoreSomeHiddenMenuItems(hiddenMenuItems, menuItemsCount, menuItemIds);
            }
        }

        private void RestoreAllHiddenMenuItems(Dictionary<int, PieMenuItem> hiddenMenuItems, int menuItemsCount)
        {
            for (int i = 0; i < menuItemsCount; i++)
            {
                if (hiddenMenuItems.ContainsKey(i))
                {
                    Restore(hiddenMenuItems, i);
                }
                else
                {
                    restoredMenuItems.Add(i, tracker.GetMenuItem(i));
                }
            }

            MoveMenuItems(restoredMenuItems, pieMenuElements.MenuItemsDir);
        }

        private void RestoreSomeHiddenMenuItems(Dictionary<int, PieMenuItem> hiddenMenuItems, int menuItemsCount,
            List<int> menuItemIds)
        {
            ValidateMenuItemIds(hiddenMenuItems, menuItemIds);
            menuItemIds.Sort();

            int menuItemIdsCurrentIndex = 0;
            for (int i = 0; i < menuItemsCount; i++)
            {
                // This condition checks if the current 'i' value matches one of the 'menuItemIds'
                // and ensures that 'menuItemIdsCurrentIndex' is within a valid range to avoid errors.
                if (menuItemIdsCurrentIndex < menuItemIds.Count && i == menuItemIds[menuItemIdsCurrentIndex])
                {
                    Restore(hiddenMenuItems, i);
                    menuItemIdsCurrentIndex++;
                }
                else
                {
                    PieMenuItem menuItem = tracker.GetMenuItem(i);
                    if (menuItem != null)
                        restoredMenuItems.Add(i, menuItem);
                }
            }

            MoveMenuItems(restoredMenuItems, pieMenuElements.MenuItemsDir);
        }

        private void ValidateMenuItemIds(Dictionary<int, PieMenuItem> hiddenMenuItems, List<int> menuItemIds)
        {
            if (menuItemIds.Count > hiddenMenuItems.Count)
            {
                throw new Exception(
                    "The list of Menu Items to restore you provided is longer than the actual number of hidden Menu Items.");
            }
        }

        private void Restore(Dictionary<int, PieMenuItem> hiddenMenuItems, int index)
        {
            if (hiddenMenuItems.ContainsKey(index))
            {
                restoredMenuItems.Add(index, hiddenMenuItems[index]);
                hiddenMenuItems.Remove(index);
            }
            else
                throw new Exception($"The Menu Item with id: {index} is not hidden.");
        }

        private void MoveMenuItems(Dictionary<int, PieMenuItem> menuItems, Transform newParent)
        {
            foreach (var menuItem in menuItems)
            {
                menuItem.Value.transform.SetParent(newParent);
            }
        }

        private void GetComponents(PieMenu pieMenu)
        {
            pieMenuElements = pieMenu.PieMenuElements;
            tracker = pieMenu.MenuItemsTracker;
            menuItems = tracker.PieMenuItems;
        }
    }
}
