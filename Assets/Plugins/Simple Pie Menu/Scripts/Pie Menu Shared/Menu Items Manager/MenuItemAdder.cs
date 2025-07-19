using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(PieMenuDrawer))]
    public class MenuItemAdder : MonoBehaviour
    {
        private PieMenuDrawer drawer;

        private void OnEnable()
        {
            drawer = GetComponent<PieMenuDrawer>();
        }

        public void Add(PieMenu pieMenu, List<GameObject> menuItems)
        {
            MenuItemSelectionHandler selectionHandler = pieMenu.SelectionHandler;

            selectionHandler.ToggleSelection(false);

            foreach (GameObject item in menuItems)
            {
                Transform menuItemsDir = pieMenu.PieMenuElements.MenuItemsDir;
                Instantiate(item, menuItemsDir);
                pieMenu.MenuItemsTracker.Initialize(menuItemsDir);
            }

            drawer.Redraw(pieMenu);
            selectionHandler.ToggleSelection(true);
        }
    }
}
