using SimplePieMenu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowPieMenu : MonoBehaviour
{
    public InputActionReference rightClick;
    [SerializeField] PieMenu pieMenu;
    private PieMenuDisplayer displayer;
    [SerializeField]
    private GameObject clickBlocker;
    [SerializeField]
    private GraphicRaycaster graphicRaycaster;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        displayer = GetComponent<PieMenuDisplayer>();
    }

    private void OnEnable()
    {
        rightClick.action.performed += OnRightClick;
        rightClick.action.Enable();
        PieMenuShared.References.PieMenuToggler.OnMenuStateChanged += PieMenuStateChanged;
    }

    private void OnDisable()
    {
        rightClick.action.performed -= OnRightClick;
        rightClick.action.Disable();
        PieMenuShared.References.PieMenuToggler.OnMenuStateChanged -= PieMenuStateChanged;
    }
    private void PieMenuStateChanged(bool isOpen)
    {
        return;
        if (isOpen == false)
        {
            HideMenuItem();
        }
        else
        {
            List<int> menuItemsIds = new() { 0, 1, 2, 3 };
            PieMenuShared.References.MenuItemsManager.MenuItemHider.Restore(pieMenu, menuItemsIds);

        }
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        displayer.ShowPieMenu(pieMenu);
    }

    [ContextMenu("asdsad")]
    private void HideMenuItem()
    {
        List<int> menuItemsIds = new() { 1, 2, 3, 4 };

        MenuItemDisabler disabler =
PieMenuShared.References.MenuItemsManager.MenuItemDisabler;

        for (int i = 0; i < 5; i++)
        {
            disabler.ToggleDisable(pieMenu, i, disabler);
        }


        //
        //PieMenuShared.References.MenuItemsManager.MenuItemHider.Hide(pieMenu,
        //menuItemsIds);
    }




    private void OnApplicationFocus(bool focus)
    {

        //if (focus == false)
        //{
        //    //pieMenu.gameObject.SetActive(false);
        //    PieMenuShared.References.PieMenuToggler.SetActive(pieMenu, false);
        //}
    }
}

