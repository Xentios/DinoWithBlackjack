using UnityEngine;

namespace SimplePieMenu
{
    public class IconScaler : MonoBehaviour
    {
        public void ChangeScale(Transform icon, float newScale)
        {
            icon.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
