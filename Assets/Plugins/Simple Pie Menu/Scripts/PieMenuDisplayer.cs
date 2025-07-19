using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuDisplayer : MonoBehaviour
    {
        public void ShowPieMenu(PieMenu pieMenu)
        {
            if (pieMenu != null)
            {
                PieMenuInfo pieMenuInfo = pieMenu.PieMenuInfo;
                if (pieMenuInfo != null && !pieMenuInfo.IsActive && !pieMenuInfo.IsTransitioning)
                {            
                    PieMenuShared.References.PieMenuToggler.SetActive(pieMenu, true);
                }
                else if(pieMenuInfo == null) InitializePieMenu(pieMenu);
            }
        }
        private void InitializePieMenu(PieMenu pieMenu)
        {
            pieMenu.transform.parent.gameObject.SetActive(true);
            pieMenu.gameObject.SetActive(true);
        }
    }
}

