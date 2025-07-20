using SimplePieMenu;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowPieMenu : MonoBehaviour
{
    public InputActionReference rightClick;
    [SerializeField] PieMenu pieMenu;
    private PieMenuDisplayer displayer;

    private void Awake()
    {
        displayer = GetComponent<PieMenuDisplayer>();
    }


    private void OnEnable()
    {
        rightClick.action.performed += OnRightClick;
        rightClick.action.Enable();
    }

    private void OnDisable()
    {
        rightClick.action.performed -= OnRightClick;
        rightClick.action.Disable();
    }


    private void OnRightClick(InputAction.CallbackContext context)
    {
        displayer.ShowPieMenu(pieMenu);
    }
}

