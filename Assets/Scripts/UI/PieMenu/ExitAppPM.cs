using SimplePieMenu;
using UnityEngine;

public class ExitAppPM : MonoBehaviour, IMenuItemClickHandler

{
    public void Handle()
    {
        Application.Quit();
    }
}
