using UnityEngine;

namespace SimplePieMenu
{
    public class InfoPanelResizer : MonoBehaviour
    {
        public void Resize(PieMenu pieMenu, float newScale)
        {
            if (pieMenu.PieMenuInfo.InfoPanelEnabled)
            {
                PieMenuShared.References.InfoPanelSettingsHandler.ChangeScale(pieMenu, newScale);

                PieMenuInfoPanelSettings infoPanelSettings = pieMenu.transform.GetComponent<PieMenuInfoPanelSettings>();
                infoPanelSettings.SetScale(newScale);
            }
        }
    }
}
