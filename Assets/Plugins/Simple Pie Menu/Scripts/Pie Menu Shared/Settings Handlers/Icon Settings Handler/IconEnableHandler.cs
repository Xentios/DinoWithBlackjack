using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(IconCreator))]
    [RequireComponent(typeof(IconDirRotationCalculator))]
    [RequireComponent(typeof(IconRotator))]
    [RequireComponent(typeof(IconResizer))]
    [ExecuteInEditMode]
    public class IconEnableHandler : MonoBehaviour
    {
        public IconCreator IconCreator { get; private set; }
        public IconDirRotationCalculator IconDirRotationCalculator { get; private set; }
        public IconRotator IconRotator { get; private set; }
        public IconResizer Resizer { get; private set; }


        private void OnEnable()
        {
            InitializeComponents();
        }

        public void Handle(PieMenu pieMenu, bool addIcons)
        {
            pieMenu.PieMenuInfo.SetIconsEnabled(addIcons);

            ApplyChangesToExistingMenuItems(pieMenu, addIcons);

            Resizer.Resize(pieMenu, pieMenu.PieMenuInfo.Scale);
        }

        public void HandleRotation(PieMenu pieMenu, bool iconsEnabled)
        {
            if (iconsEnabled)
            {
                Quaternion iconDirRotation = CalculateIconDirRotation(pieMenu, iconsEnabled);

                foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
                {
                    IconDirRotationCalculator.Rotate(menuItem.Value.transform, iconDirRotation);
                }

                IconRotator.Rotate(pieMenu.GetMenuItems(), iconDirRotation);
            }
        }

        public Quaternion CalculateIconDirRotation(PieMenu pieMenu, bool iconsEnabled)
        {
            Quaternion iconDirRotation = Quaternion.identity;

            if (iconsEnabled)
            {
                iconDirRotation = IconDirRotationCalculator.Calculate(pieMenu.PieMenuInfo.MenuItemFillAmount);
            }

            return iconDirRotation;
        }

        private void ApplyChangesToExistingMenuItems(PieMenu pieMenu, bool addIcons)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
            {
                IconCreator.CreateOrDestroyIcon(addIcons, menuItem.Value.transform);
            }

            HandleRotation(pieMenu, addIcons);
        }

        private void InitializeComponents()
        {
            IconCreator = GetComponent<IconCreator>();
            IconDirRotationCalculator = GetComponent<IconDirRotationCalculator>();
            IconRotator = GetComponent<IconRotator>();
            Resizer = GetComponent<IconResizer>();
        }
    }
}
