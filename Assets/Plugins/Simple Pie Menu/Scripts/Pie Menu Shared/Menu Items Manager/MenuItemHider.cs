using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(PieMenuDrawer))]
    [RequireComponent(typeof(MenuItemRestorer))]
    public class MenuItemHider : MonoBehaviour
    {
        private PieMenuDrawer drawer;
        private MenuItemRestorer menuItemRestorer;

        private void OnEnable()
        {
            drawer = GetComponent<PieMenuDrawer>();
            menuItemRestorer = GetComponent<MenuItemRestorer>();
        }

        public void Hide(PieMenu pieMenu, List<int> menuItemIds)
        {
            pieMenu.SelectionHandler.ToggleSelection(false);

            HideMenuItems(pieMenu, menuItemIds);
            drawer.Redraw(pieMenu);

            pieMenu.SelectionHandler.ToggleSelection(true);
        }

        public void Restore(PieMenu pieMenu, List<int> menuItemIds = null)
        {
            pieMenu.SelectionHandler.ToggleSelection(false);

            menuItemRestorer.RestoreMenuItems(pieMenu, menuItemIds);

            drawer.Redraw(pieMenu);

            pieMenu.SelectionHandler.ToggleSelection(true);
        }

        private void HideMenuItems(PieMenu pieMenu, List<int> menuItemIds)
        {
            foreach (int id in menuItemIds)
            {
                MenuItemsTracker tracker = pieMenu.MenuItemsTracker;

                PieMenuItem menuItem = tracker.GetMenuItem(id);

                if (menuItem == null)
                    Debug.Log("Could not find menu item with given id");
                else
                {
                    if (tracker.HiddenMenuItems.ContainsKey(id)) continue;

                    if (pieMenu.MenuItemsTracker.PieMenuItems.Count != 1)
                    {
                        tracker.HiddenMenuItems.Add(id, menuItem);
                        menuItem.transform.SetParent(pieMenu.PieMenuElements.HiddenMenuItemsDir);
                    }
                    else
                        throw new Exception(
                            $"You are trying to hide the last Menu Item. This is not allowed as one must always remain.");
                }
            }
        }
    }
}
