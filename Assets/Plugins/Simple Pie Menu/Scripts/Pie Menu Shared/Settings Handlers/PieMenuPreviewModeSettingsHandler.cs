using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuPreviewModeSettingsHandler : MonoBehaviour
    {
        private PieMenu pieMenuReference;

        private bool updateEnabled;
        private bool executing;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            ShowPieMenuAgainAfterClosingIt();
        }

        public void HandleEnableValueChange(PieMenu pieMenu, bool testModeEnabled)
        {
            pieMenuReference = pieMenu;
            enabled = testModeEnabled;
        }

        private void ShowPieMenuAgainAfterClosingIt()
        {
            bool isActive = pieMenuReference.PieMenuInfo.IsActive;

            if (isActive) updateEnabled = true;

            if (updateEnabled)
            {
                if (!isActive && !executing)
                {
                    updateEnabled = false;
                    executing = true;
                    ShowPieMenu();
                }
            }
        }

        private void ShowPieMenu()
        {
            PieMenuShared.References.PieMenuToggler.SetActive(pieMenuReference, true);
            executing = false;
        }
    }

}
