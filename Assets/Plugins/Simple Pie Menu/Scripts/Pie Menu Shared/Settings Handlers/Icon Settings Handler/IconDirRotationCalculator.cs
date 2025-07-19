using UnityEngine;

namespace SimplePieMenu
{
    public class IconDirRotationCalculator : MonoBehaviour
    {
        //Positioning the icon within the button involves calculating the appropriate rotation for the icon directory.
        //This result is also needed to calculate the rotation for individual icons.

        public Quaternion Calculate(float buttonFillAmount)
        {
            float circleDegrees = 360f;
            float buttonSize = (buttonFillAmount * circleDegrees);
            float centreOfTheButton = buttonSize / 2f;

            float zAxisRotation = -centreOfTheButton;

            Quaternion iconDirRotation = Quaternion.Euler(0f, 0f, zAxisRotation);

            return iconDirRotation;
        }

        public void Rotate(Transform menuItem, Quaternion rotation)
        {
            int iconDirIndex = 0;
            Transform iconDir = menuItem.GetChild(iconDirIndex);

            Quaternion parentRotation = menuItem.rotation;
            Quaternion newRotation = parentRotation * rotation;

            iconDir.rotation = newRotation;
        }
    }
}
