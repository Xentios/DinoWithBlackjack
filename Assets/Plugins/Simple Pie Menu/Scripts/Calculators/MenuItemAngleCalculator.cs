using UnityEngine;

namespace SimplePieMenu
{
    public class MenuItemAngleCalculator : MonoBehaviour
    {
        public static void Calculate(PieMenu pieMenu)
        {
            int circleDegrees = 360;
            int menuItemCount = pieMenu.MenuItemsTracker.PieMenuItems.Count;
            int menuItemSpacing = pieMenu.PieMenuInfo.MenuItemSpacing;

            int totalSpacing = MenuItemSpacingCalculator.CalculateTotalSpacing(menuItemCount, menuItemSpacing);

            int totalMenuItemsAngle = circleDegrees - totalSpacing;
            int menuItemAngle = totalMenuItemsAngle / menuItemCount;

            if (menuItemAngle < 0) menuItemAngle = 0;
            pieMenu.PieMenuInfo.SetMenuItemAngle(menuItemAngle);
        }
    }
}
