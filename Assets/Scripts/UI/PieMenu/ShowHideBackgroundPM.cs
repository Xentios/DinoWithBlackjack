using SimplePieMenu;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideBackgroundPM : MonoBehaviour, IMenuItemClickHandler
{
    [SerializeField]
    private bool workWithInvisableBackground = false;
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private Image backgroundImage;

    private bool isVisible = true;
    public void Handle()
    {
        isVisible = !isVisible;
        if (workWithInvisableBackground == false)
        {
            background.SetActive(isVisible);
            backgroundImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            background.SetActive(true);
            float alpha = isVisible ? 1 : 0;
            backgroundImage.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}


