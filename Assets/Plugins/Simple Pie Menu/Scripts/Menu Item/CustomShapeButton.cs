using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    public class CustomShapeButton : MonoBehaviour
    {
        void Start()
        {
            // The alphaHitTestMinimumThreshold property is used to determine the minimum alpha value required for
            // the image to be considered for hit testing. In other words, it controls how transparent or opaque a
            // part of the image needs to be in order for it to respond to input events(e.g., mouse clicks or touch events).

            this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}
