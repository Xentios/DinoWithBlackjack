using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(IconScaler))]
    [ExecuteInEditMode]
    public class IconScaleChangeHandler : MonoBehaviour
    {
        private IconScaler iconScaler;

        private void OnEnable()
        {
            iconScaler = GetComponent<IconScaler>();
        }

        public void Handle(PieMenu pieMenu, IconGetter iconGetter, float iconScale)
        {
            ChangeMenuItemsScale(pieMenu, iconGetter, iconScale);
        }

        private void ChangeMenuItemsScale(PieMenu pieMenu, IconGetter iconGetter, float iconScale)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
            {
                iconScaler.ChangeScale(iconGetter.GetIcon(menuItem.Value.transform), iconScale);
            }
        }
    }
}
