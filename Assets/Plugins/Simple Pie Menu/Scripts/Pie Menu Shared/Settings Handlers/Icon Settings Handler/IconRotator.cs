using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    public class IconRotator : MonoBehaviour
    {
        public void Rotate(Dictionary<int, PieMenuItem> menuItems, Quaternion iconDirRotation)
        {
            //The first element in the list of icons requires a different mathematical pattern than the rest.
            RotateFirstElement(menuItems[0].transform, iconDirRotation);
            RotateOtherElements(menuItems, iconDirRotation);
        }

        private static void RotateFirstElement(Transform menuItem, Quaternion iconDirRotation)
        {
            Quaternion firstIconRotation = Quaternion.Euler(0f, 0f, Mathf.Abs(iconDirRotation.z));
            Transform firstIconDir = menuItem.GetChild(0).transform;
            Transform firstIcon = firstIconDir.GetChild(0).transform;
            firstIcon.rotation = firstIconRotation;
        }

        private static void RotateOtherElements(Dictionary<int, PieMenuItem> menuItems, Quaternion iconDirRotation)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in menuItems)
            {
                float menuItemRotationZ = menuItem.Value.transform.rotation.z;
                float iconRotationZ = -(menuItemRotationZ - iconDirRotation.z);
                Transform iconDir = menuItem.Value.transform.GetChild(0).transform;
                Transform icon = iconDir.GetChild(0).transform;
                icon.rotation = Quaternion.Euler(0f, 0f, iconRotationZ);
            }
        }
    }
}
