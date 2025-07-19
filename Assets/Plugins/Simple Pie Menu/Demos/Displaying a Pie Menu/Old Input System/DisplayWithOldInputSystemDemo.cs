using UnityEngine;

namespace SimplePieMenu
{
    public class DisplayWithOldInputSystemDemo : MonoBehaviour
    {
        [SerializeField] KeyCode displayButton;
        [SerializeField] PieMenu pieMenu;

        private PieMenuDisplayer displayer;

        private void Awake()
        {
#if ENABLE_INPUT_SYSTEM
            Debug.Log("This demo won't work if you are using only the New Input System." +
                " Please set 'Active Input Handling' to the Old Input Manager or use the 'Both' option.");         
#endif

            displayer = GetComponent<PieMenuDisplayer>();
        }

        private void Update()
        {
            Display();
        }

        private void Display()
        {
            if (Input.GetKeyDown(displayButton))
            {
                displayer.ShowPieMenu(pieMenu);
            }
        }
    }
}