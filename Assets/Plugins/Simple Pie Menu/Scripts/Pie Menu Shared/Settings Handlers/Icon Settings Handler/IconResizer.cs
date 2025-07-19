using UnityEngine;

namespace SimplePieMenu
{
    public class IconResizer : MonoBehaviour
    {

        public void Resize(PieMenu pieMenu, float newScale)
        {
            if (pieMenu.PieMenuInfo.IconsEnabled)
            {
                MenuItemIconsSettings iconsSettings = pieMenu.transform.GetComponent<MenuItemIconsSettings>();
                MenuItemIconsSettingsHandler iconsSettingsHandler = PieMenuShared.References.IconsSettingsHandler;

                iconsSettings.SetScale(newScale);
                iconsSettingsHandler.HandleIconScaleChange(pieMenu, newScale);

                Transform iconDir = iconsSettingsHandler.EnableHandler.IconCreator.IconDir.transform;
                int initialOffset = GetInitialOffset(iconDir);

                int newOffset = CalculateNewOffset(pieMenu, initialOffset);

                iconsSettings.SetOffset(newOffset);
                iconsSettingsHandler.HandleIconOffsetChange(pieMenu, newOffset);
            }
        }

        public int GetInitialOffset(Transform iconDir)
        {
            Transform icon = iconDir.GetChild(0);
            int currentOffset = (int)icon.localPosition.x;
            return currentOffset;
        }

        private int CalculateNewOffset(PieMenu pieMenu, float initialOffset)
        {
            int initialSize = pieMenu.PieMenuInfo.MenuItemInitialSize;
            int currentSize = pieMenu.PieMenuInfo.MenuItemSize;

            float newOffset = (initialOffset / initialSize) * currentSize;
            return Mathf.RoundToInt(newOffset);
        }
    }
}
