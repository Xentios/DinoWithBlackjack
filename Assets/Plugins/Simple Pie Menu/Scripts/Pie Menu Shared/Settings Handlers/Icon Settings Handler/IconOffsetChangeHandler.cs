using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(IconMover))]
    [ExecuteInEditMode]
    public class IconOffsetChangeHandler : MonoBehaviour
    {
        private IconMover iconMover;

        private void OnEnable()
        {
            iconMover = GetComponent<IconMover>();
        }

        public void Handle(PieMenu pieMenu, IconGetter iconGetter, int offsetFromCenter)
        {
            ChangeIconsPosition(pieMenu, iconGetter, offsetFromCenter);
        }

        private void ChangeIconsPosition(PieMenu pieMenu, IconGetter iconGetter, int offsetFromCenter)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
            {
                iconMover.Move(iconGetter.GetIcon(menuItem.Value.transform), offsetFromCenter);
            }
        }
    }
}
