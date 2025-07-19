using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    public class PieMenuBackgroundSettingsHandler : MonoBehaviour
    {
        public void SetActive(Image background, bool isActive)
        {
            background.gameObject.SetActive(isActive);
        }

        public void ChangeColor(Image background, Color newColor)
        {
            background.color = newColor;
        }
    }
}
