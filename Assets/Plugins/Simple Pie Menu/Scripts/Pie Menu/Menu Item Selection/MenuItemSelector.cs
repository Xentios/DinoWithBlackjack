using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimplePieMenu
{
    public class MenuItemSelector : MonoBehaviour
    {
        public Dictionary<int, Button> ButtonComponentsReference { get; private set; }
        public Dictionary<int, PieMenuItem> PieMenuItemsReference { get; private set; }

        private int previousSelection;

        public void ResetPreviousSelection()
        {
            previousSelection = -1;
        }

        public void RegisterMenuItems(Dictionary<int, Button> buttonComponents,
            Dictionary<int, PieMenuItem> pieMenuItems)
        {
            ButtonComponentsReference = buttonComponents;
            PieMenuItemsReference = pieMenuItems;
        }

        public void SelectMenuItem(PieMenu pieMenu, int selection)
        {
            if (selection != previousSelection)
            {
                UnselectPreviousMenuItem();

                ButtonComponentsReference[selection].Select();
                PieMenuItemsReference[selection].OnPointerEnter();

                previousSelection = selection;
            }
        }

        public void UnselectPreviousMenuItem()
        {
            if (previousSelection != -1)
            {
                PieMenuItemsReference[previousSelection].BeforePointerExit();
                PieMenuItemsReference[previousSelection].OnPointerExit();
                EventSystem.current.SetSelectedGameObject(null);

                previousSelection = -1;
            }
        }
    }
}