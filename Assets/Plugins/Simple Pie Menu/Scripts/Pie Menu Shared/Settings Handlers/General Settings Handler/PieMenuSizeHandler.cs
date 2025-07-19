using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuSizeHandler : MonoBehaviour
    {
        public static float CalculatePieMenuScale(PieMenu pieMenu, int size)
        {
            int initialSize = pieMenu.PieMenuInfo.MenuItemInitialSize;

            float newScale = (float)size / initialSize;
            return newScale;
        }

        public void Handle(PieMenu pieMenu, int size)
        {
            pieMenu.PieMenuInfo.SetMenuItemSize(size);

            ChangeMenuItemsSize(pieMenu, size);

            float newScale = CalculatePieMenuScale(pieMenu, size);
            pieMenu.PieMenuInfo.SetScale(newScale);

            PieMenuShared.References.InfoPanelSettingsHandler.Resizer.Resize(pieMenu, newScale);

            PieMenuShared.References.IconsSettingsHandler.EnableHandler.Resizer.Resize(pieMenu, newScale);

            UpdateSelectionHandler(pieMenu);
        }

        private void UpdateSelectionHandler(PieMenu pieMenu)
        {
            if (pieMenu.PieMenuInfo.SelectionConstrained)
            {
                PieMenuSelectionSettingsHandler selectionHandler = PieMenuShared.References.SelectionSettingsHandler;
                float maxDistance = selectionHandler.CalculateConstraintMaxDistance(pieMenu);
                pieMenu.SelectionHandler.SelectionCalculator.SetContraintMaxDistance((int)maxDistance);
            }
        }

        private void ChangeMenuItemsSize(PieMenu pieMenu, int size)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
            {
                ChangeSize(menuItem.Value.transform, size);
            }
        }

        private void ChangeSize(Transform menuItem, int size)
        {
            Image image = menuItem.GetComponent<Image>();

            RectTransform rectTransform = image.rectTransform;
            rectTransform.sizeDelta = new Vector2(size, size);
        }
    }
}
