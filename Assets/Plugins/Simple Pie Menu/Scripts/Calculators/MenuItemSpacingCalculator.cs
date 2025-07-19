
using UnityEngine;

namespace SimplePieMenu
{
    public class MenuItemSpacingCalculator : MonoBehaviour
    {
        public static int CalculateTotalSpacing(int menuItemCount, int menuItemSpacing)
        {
            int totalSpacing = menuItemCount * menuItemSpacing;
            return totalSpacing;
        }

        public static float CalculatTotalSpacingPercentage(int menuItemCount, int menuItemSpacing)
        {
            float circleDegrees = 360f;
            float totalSpacing = CalculateTotalSpacing(menuItemCount, menuItemSpacing);
            float totalSpacingToPercentage = totalSpacing / circleDegrees;
            return totalSpacingToPercentage;
        }
    }
}
