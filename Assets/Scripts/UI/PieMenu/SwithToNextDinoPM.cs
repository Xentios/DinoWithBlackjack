using SimplePieMenu;
using UnityEngine;

public class SwithToNextDinoPM : MonoBehaviour, IMenuItemClickHandler
{
    [SerializeField]
    private Transform dinoHolder;

    private int activeChildIndex;
    public void Handle()
    {
        dinoHolder.GetChild(activeChildIndex).gameObject.SetActive(false);
        activeChildIndex++;
        activeChildIndex %= dinoHolder.childCount;
        dinoHolder.GetChild(activeChildIndex).gameObject.SetActive(true);

    }
}
