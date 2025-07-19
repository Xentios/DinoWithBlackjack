using UnityEngine;

namespace SimplePieMenu
{
    public class SelectionCalculator : MonoBehaviour
    {
        private bool selectionConstrained;
        private int constraintMaxDistance;

        private readonly int circleDegrees = 360;

        public void ToggleSelectionConstraint(bool selectionConstrained)
        {
            this.selectionConstrained = selectionConstrained;
        }

        public void SetContraintMaxDistance(int maxDistance)
        {
            constraintMaxDistance = maxDistance;
        }

        public int CalculateSelection(PieMenu pieMenu, IInputDevice inputDevice)
        {
            int selection;

            // This method calculates the angle from 0 to 360 degrees based on the mouse position relative to the center of the pie menu.
            float currentAngle = CalculateAngle(inputDevice, pieMenu.PieMenuInfo.AnchoredPosition);
            if (currentAngle != -1)
            {
                // This method takes into account the pie menu rotation to addjust currentAngle
                float addjustedAngle = AddjustAngle(pieMenu, currentAngle);

                int menuItemCount = pieMenu.MenuItemsTracker.PieMenuItems.Count;
                selection = (int)addjustedAngle / (circleDegrees / menuItemCount);

                // This method adjusts the Menu Item selection taking into account their spacing.
                selection = AdjustSelection(pieMenu, selection, currentAngle, addjustedAngle);

                //This calculation reverses the selection direction. Without it, the items would be selected clockwise,
                //starting from the left-center of the circle(in our case, the items should be selected in the opposite direction).
                selection = menuItemCount - selection;

                //For a 360 degree angle, the calculation result is incorrect, and it should be adjusted like this:
                if (selection >= menuItemCount) selection = 0;

            }
            else
                selection = -1;

            return selection;
        }

        private float CalculateAngle(IInputDevice inputDevice, Vector2 anchoredPosition)
        {
            Vector2 inputOnScreenPosition = inputDevice.GetPosition(anchoredPosition);
            if (selectionConstrained)
            {

                float distance = inputOnScreenPosition.magnitude;
                if (distance < constraintMaxDistance)
                    return -1;
            }

            float currentAngle = Mathf.Atan2(inputOnScreenPosition.y, -inputOnScreenPosition.x) * Mathf.Rad2Deg;
            currentAngle = (currentAngle + circleDegrees) % circleDegrees;
            return currentAngle;
        }

        private float AddjustAngle(PieMenu pieMenu, float currentAngle)
        {
            PieMenuInfo pieMenuInfo = pieMenu.PieMenuInfo;
            float addjustedAngle = (currentAngle + pieMenuInfo.Rotation + circleDegrees) % circleDegrees;
            return addjustedAngle;
        }

        private int AdjustSelection(PieMenu pieMenu, int selection, float currentAngle, float addjustedAngle)
        {
            PieMenuInfo pieMenuInfo = pieMenu.PieMenuInfo;
            float menuItemSpacing = pieMenuInfo.MenuItemSpacing;
            int minSpacing = 1;
            if (menuItemSpacing <= minSpacing) return selection;

            // Calculate the start and end angles of the current Menu Item, considering spacing and rotation.
            float menuItemDegrees = pieMenuInfo.MenuItemDegrees;
            float menuItemStartAngle = (menuItemDegrees + menuItemSpacing) * selection;
            float menuItemEndAngle = menuItemStartAngle + menuItemDegrees;

            float halfOfTheMenuItemSpacing = menuItemSpacing / 2;
            menuItemStartAngle -= halfOfTheMenuItemSpacing;
            menuItemEndAngle += halfOfTheMenuItemSpacing;

            int pieMenuRotation = pieMenuInfo.Rotation;
            menuItemStartAngle += circleDegrees - pieMenuRotation;
            menuItemEndAngle += circleDegrees - pieMenuRotation;

            menuItemStartAngle = (menuItemStartAngle + circleDegrees) % circleDegrees;
            menuItemEndAngle = (menuItemEndAngle + circleDegrees) % circleDegrees;

            // Checking if the current angle is within the calculated range
            if ((menuItemEndAngle < menuItemStartAngle &&
                 (currentAngle >= menuItemStartAngle || currentAngle <= menuItemEndAngle)) ||
                (currentAngle >= menuItemStartAngle && currentAngle <= menuItemEndAngle))
            {
                return selection; // No adjustment needed
            }
            else
            {
                // Adjusting selection and handling wrap-around
                selection++;
                int firstIndex = 0;
                int menuItemsCount = pieMenu.MenuItemsTracker.PieMenuItems.Count;
                if (selection >= menuItemsCount) selection = firstIndex;
                return selection;
            }

        }

    }
}

