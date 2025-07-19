using SimplePieMenu;
using UnityEngine;

public class HidePieMenuPM : MonoBehaviour, IMenuItemClickHandler
{
    [SerializeField]
    private PieMenu pieMenu;
    [SerializeField]
    private PieMenuDisplayer displayer;

    void IMenuItemClickHandler.Handle()
    {
        //displayer.ShowPieMenu(pieMenu);
    }


}
