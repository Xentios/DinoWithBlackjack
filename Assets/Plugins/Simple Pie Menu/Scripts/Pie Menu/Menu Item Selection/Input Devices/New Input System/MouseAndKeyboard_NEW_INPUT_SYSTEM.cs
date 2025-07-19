using UnityEngine;

namespace SimplePieMenu
{
    public class MouseAndKeyboard_NEW_INPUT_SYSTEM: MonoBehaviour, IInputDevice
    {
        private PieMenuControls pieMenuControls;

        private Vector2 cursorPosition;
        private bool isSelectionCanceled;
        private bool isReturnCanceled;

        private void Awake()
        {
            pieMenuControls = new PieMenuControls();

            pieMenuControls.MouseAndKeyboard.PointerPosition.performed += ctx => OnCursorPositionPerformed(ctx.ReadValue<Vector2>());

            //canceled event is triggered when the button is released
            pieMenuControls.MouseAndKeyboard.Selection.canceled += _ => OnSelectionCanceled();
            pieMenuControls.MouseAndKeyboard.Close.canceled += _ => OnCloseCanceled();

            pieMenuControls.Enable();
        }

        private void OnDestroy()
        {
            pieMenuControls.Disable();
        }

        public Vector2 GetPosition(Vector2 anchoredPosition)
        {
            Vector2 position;

            position.x = cursorPosition.x - (Screen.width / 2f) - anchoredPosition.x;
            position.y = cursorPosition.y - (Screen.height / 2f) - anchoredPosition.y;

            return position;
        }

        public bool IsSelectionButtonPressed()
        {
            return IsButtonPressed(ref isSelectionCanceled);
        }

        public bool IsCloseButtonPressed()
        {
            return IsButtonPressed(ref isReturnCanceled);
        }

        private bool IsButtonPressed(ref bool buttonCanceled)
        {
            if (buttonCanceled)
            {
                buttonCanceled = false;
                return true;
            }
            else
                return false;
            
        }

        private void OnCursorPositionPerformed(Vector2 pointerPosition)
        {
            cursorPosition = pointerPosition;
        }

        private void OnSelectionCanceled()
        {
            isSelectionCanceled = true;
        }

        private void OnCloseCanceled()
        {
            isReturnCanceled = true;
        }
    }
}
