using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(InfoPanelResizer))]
    [ExecuteInEditMode]
    public class PieMenuInfoPanelSettingsHandler : MonoBehaviour
    {
        public InfoPanelResizer Resizer { get; private set; }

        private void OnEnable()
        {
            Resizer = GetComponent<InfoPanelResizer>();
        }

        public void HandleEnableValueChange(PieMenu pieMenu, bool enabled)
        {
            pieMenu.PieMenuInfo.SetInfoPanelEnabled(enabled);
            Resizer.Resize(pieMenu, pieMenu.PieMenuInfo.Scale);
            SetActive(pieMenu, enabled);
        }

        public void SetActive(PieMenu pieMenu, bool isActive)
        {
            pieMenu.PieMenuElements.InfoPanel.gameObject.SetActive(isActive);
        }

        public void ChangeHeaderColor(PieMenu pieMenu, Color newColor)
        {
            pieMenu.PieMenuElements.Header.color = newColor;
        }

        public void ChangeDetailsColor(PieMenu pieMenu, Color newColor)
        {
            pieMenu.PieMenuElements.Details.color = newColor;
        }

        public void ChangeScale(PieMenu pieMenu, float scale)
        {
            pieMenu.PieMenuElements.InfoPanel.localScale = new(scale, scale, scale);
        }

        public void ModifyHeader(PieMenu pieMenu, string newHeader)
        {
            pieMenu.PieMenuElements.Header.text = newHeader;
        }

        public void ModifyDetails(PieMenu pieMenu, string newMessage)
        {
            pieMenu.PieMenuElements.Details.text = newMessage;
        }

        public void RestoreDefaultInfoPanelText(PieMenu pieMenu)
        {
            string placeholderHeaderText = "Header";

            string placeholderDetailsText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                                            " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
                                            " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

            ModifyHeader(pieMenu, placeholderHeaderText);
            ModifyDetails(pieMenu, placeholderDetailsText);
        }
    }
}
