
using UnityEngine;

namespace SimplePieMenu
{
    public interface IInputDevice
    {
        Vector2 GetPosition(Vector2 anchoredPosition);

        bool IsSelectionButtonPressed();

        bool IsCloseButtonPressed();
    }
}
