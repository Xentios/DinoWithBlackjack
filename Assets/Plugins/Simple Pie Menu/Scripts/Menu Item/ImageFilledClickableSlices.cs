using UnityEngine;
using UnityEngine.UI;

// This code snippet is a slightly edited version of the original code, which comes from:
// https://forum.unity.com/threads/button-with-radial-fill-sprite-only-clickable-on-visible-area.546614/
// and was posted by Michael Grönert.
// His profile: https://forum.unity.com/members/badtoxic.813294/
// His discord: https://discord.gg/8QMCm2d
// It calculates the area of the image that is not visible due to fillAmount settings.

namespace SimplePieMenu
{
    public class ImageFilledClickableSlices : Image
    {
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            bool result = base.IsRaycastLocationValid(screenPoint, eventCamera);
            if (!result)
            {
                return false;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera,
                out Vector2 localPoint);

            float clickAngle = Vector2.SignedAngle(localPoint, Vector2.left);
            ;

            return (clickAngle >= 0) && (clickAngle < (360f * fillAmount));
        }
    }
}