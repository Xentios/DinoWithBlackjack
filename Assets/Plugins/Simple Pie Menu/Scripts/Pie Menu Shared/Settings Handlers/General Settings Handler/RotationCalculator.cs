using UnityEngine;

namespace SimplePieMenu
{
    public class RotationCalculator : MonoBehaviour
    {
        public static int CalculateNewRotation(int menuItemCount, int menuItemSpacing)
        {
            // The following method calculates a new rotation to ensure symmetry based on the provided number of Menu Items
            int rotation;
            int circleDegrees = 360;
            if (menuItemCount % 2 != 0)
            {
                rotation = circleDegrees - 90 / menuItemCount - menuItemSpacing / 2;
            }
            else
            {
                rotation = circleDegrees - menuItemSpacing / 2;
            }

            return rotation;
        }
    }
}
