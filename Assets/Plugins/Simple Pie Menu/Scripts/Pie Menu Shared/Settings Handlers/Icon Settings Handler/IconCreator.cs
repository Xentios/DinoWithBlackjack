using UnityEngine;

namespace SimplePieMenu
{
    public class IconCreator : MonoBehaviour
    {
        [SerializeField] GameObject iconDir;

        public GameObject IconDir
        {
            get { return iconDir; }
        }

        public void CreateOrDestroyIcon(bool addIcons, Transform menuItem)
        {
            if (addIcons)
            {
                CreateIcon(menuItem);
            }
            else
            {
                DestroyIcon(menuItem);
            }
        }

        private void CreateIcon(Transform menuItem)
        {
            GameObject iconGO = Instantiate(iconDir, menuItem);
            iconGO.name = "IconDir";
        }

        private static void DestroyIcon(Transform menuItem)
        {
            if (menuItem.childCount > 0)
            {
                GameObject iconDir = menuItem.GetChild(0).gameObject;
                DestroyImmediate(iconDir);
            }
        }
    }
}
