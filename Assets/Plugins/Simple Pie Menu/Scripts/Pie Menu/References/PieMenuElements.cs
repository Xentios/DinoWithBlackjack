using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuElements : MonoBehaviour
    {
        [SerializeField] Transform pieMenuCanvas;

        public Transform PieMenu
        {
            get { return pieMenuCanvas; }
        }


        [SerializeField] Transform menuItemsDir;

        public Transform MenuItemsDir
        {
            get { return menuItemsDir; }
        }


        [SerializeField] Transform hiddenMenuItemsDir;

        public Transform HiddenMenuItemsDir
        {
            get { return hiddenMenuItemsDir; }
        }


        [SerializeField] Image background;

        public Image Background
        {
            get { return background; }
        }


        [SerializeField] Transform infoPanel;

        public Transform InfoPanel
        {
            get { return infoPanel; }
        }


        [SerializeField] TextMeshProUGUI header;

        public TextMeshProUGUI Header
        {
            get { return header; }
        }


        [SerializeField] TextMeshProUGUI details;

        public TextMeshProUGUI Details
        {
            get { return details; }
        }


        [SerializeField] Animator animator;

        public Animator Animator
        {
            get { return animator; }
        }


        [SerializeField] AudioSource mouseClickAudioSource;

        public AudioSource MouseClickAudioSource
        {
            get { return mouseClickAudioSource; }
        }

    }
}
