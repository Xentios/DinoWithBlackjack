using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollScale : MonoBehaviour
{
    public InputActionReference scrollActionRef;
    public float scaleSpeed = 0.1f;
    public float RangeMin = 0.3f;
    public float RangeMax = 2f;

    void OnEnable()
    {
        scrollActionRef.action.Enable();
        scrollActionRef.action.performed += OnScroll;
    }

    void OnDisable()
    {
        scrollActionRef.action.performed -= OnScroll;
        scrollActionRef.action.Disable();
    }

    void OnScroll(InputAction.CallbackContext context)
    {
        Vector2 scrollDelta = context.ReadValue<Vector2>();
        float scaleChange = scrollDelta.y * scaleSpeed;
        float newScaleY = transform.localScale.y + scaleChange;

        if (newScaleY > RangeMax) newScaleY = RangeMax;
        else if (newScaleY < RangeMin) newScaleY = RangeMin;

        transform.localScale = new Vector3(newScaleY, newScaleY, newScaleY);
    }
}
