using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuCloseFunctionalitySettingsHandler : MonoBehaviour
    {
        public void Handle(PieMenu pieMenu, bool closeable)
        {
            pieMenu.PieMenuInfo.SetCloseableState(closeable);
        }
    }
}
