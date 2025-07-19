using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuPositionSettingsHandler : MonoBehaviour
    {
        public void Handle(PieMenu pieMenu, int horizontalPosition, int verticalPosition)
        {
            RectTransform rectTransform = pieMenu.GetComponent<RectTransform>();
            Vector2 anchoredPosition = rectTransform.anchoredPosition;

            anchoredPosition.Set(horizontalPosition, verticalPosition);
            rectTransform.anchoredPosition = anchoredPosition;
            pieMenu.PieMenuInfo.SetAnchoredPosition(rectTransform);
        }
    }
}
