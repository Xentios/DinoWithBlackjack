using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    public class IconGetter : MonoBehaviour
    {
        public bool CheckIfIconsAreEnabled(Transform menuItem)
        {
            if (menuItem.transform.childCount > 0) return true;
            else return false;
        }

        public Transform GetIcon(Transform menuItem)
        {
            Transform itemDir = menuItem.transform.GetChild(0);
            Transform icon = itemDir.transform.GetChild(0);
            return icon;
        }

        public void ChangeIcon(Transform menuItem, Sprite sprite)
        {
            GetIcon(menuItem).GetComponent<Image>().sprite = sprite;
        }
    }
}
