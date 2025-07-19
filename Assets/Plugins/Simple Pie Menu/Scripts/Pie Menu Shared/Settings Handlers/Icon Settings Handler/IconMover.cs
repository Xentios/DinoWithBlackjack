using UnityEngine;

namespace SimplePieMenu
{
    public class IconMover : MonoBehaviour
    {
        public void Move(Transform icon, int offsetFromCenter)
        {
            RectTransform rectTransform = icon.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector3(offsetFromCenter, 0f, 0f);
        }
    }
}
