using SimplePieMenu;
using UnityEngine;

public class ShowHideBackgroundPM : MonoBehaviour, IMenuItemClickHandler
{
    [SerializeField]
    private GameObject background;
    public void Handle()
    {
        background.SetActive(!background.activeSelf);
    }
}


