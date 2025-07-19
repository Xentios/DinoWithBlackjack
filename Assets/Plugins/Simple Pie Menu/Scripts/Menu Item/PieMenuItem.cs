using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuItem : MonoBehaviour
    {
        [SerializeField] PieMenu pieMenu;

        public PieMenu PieMenu
        {
            get { return pieMenu; }
        }


        [Header("Menu Item")] [Readonly] [SerializeField]
        int id;

        public int Id
        {
            get { return id; }
        }

        [OnValueChange(nameof(DisplayHeader))] [SerializeField]
        string header;

        public string Header
        {
            get { return header; }
        }

        [HideInInspector] [SerializeField] string details;

        public string Details
        {
            get { return details; }
        }


        public AudioSource HoverAudioSource { get; private set; }

        private Animator animator;
        private IMenuItemClickHandler clickHandler;

        private bool idAssigned;
        private bool mouseOverButton;
        private readonly string mouseEnterTrigger = "MouseEnter";
        private readonly string mouseExitTrigger = "MouseExit";

        private PieMenuInfoPanelSettingsHandler infoPanelHandler;
        private PieMenuAudioSettingsHandler audioHandler;
        private PieMenuToggler pieMenuToggler;

        private void OnEnable()
        {
            if (PrefabIsolationModeHelper.IsInPrefabIsolationMode()) return;

            GetPieMenuScript();
            InitializeComponents();
            InitializePlaymodeComponents();
        }

        public void SetId(int newId)
        {
            if (idAssigned) return;
            else
            {
                id = newId;
                idAssigned = true;
            }
        }

        public void SetHeader(string newHeader)
        {
            header = newHeader;
        }

        public void SetDetails(string newDetails)
        {
            details = newDetails;
        }

        public void DisplayHeader()
        {
            if (infoPanelHandler != null)
                infoPanelHandler.ModifyHeader(PieMenu, header);
        }

        public void DisplayDetails()
        {
            if (infoPanelHandler != null)
                infoPanelHandler.ModifyDetails(PieMenu, details);
        }

        public void OnPointerEnter()
        {
            if (mouseOverButton) return;

            mouseOverButton = true;

            audioHandler.PlayAudio(HoverAudioSource);
            animator.SetTrigger(mouseEnterTrigger);

            DisplayHeader();
            DisplayDetails();

        }

        public void BeforePointerExit()
        {
            mouseOverButton = false;
        }

        public void OnPointerExit()
        {
            if (mouseOverButton) return;
            animator.SetTrigger(mouseExitTrigger);
        }

        public void OnClick()
        {
            if (clickHandler != null)
            {
                BeforePointerExit();

                clickHandler.Handle();
            }
            else
            {
                Debug.Log("To handle clicks, you need to create a new script in which you implement the IMenuItemClickHandler interface." +
                    " Then, attach it to the appropriate Menu Item. Check the documentation to learn more.");
            }

            pieMenuToggler.SetActive(PieMenu, false);
        }

        private void GetPieMenuScript()
        {
            if (pieMenu == null)
            {
                pieMenu = GetComponentInParent<PieMenu>();
            }
        }

        private void InitializeComponents()
        {
            if (PieMenuShared.Instance != null)
            {
                GetPieMenuItemComponents();
            }
            else if (pieMenu != null)
            {
                PieMenu.OnComponentsInitialized += GetPieMenuItemComponents;
            }
        }

        private void GetPieMenuItemComponents()
        {
            HoverAudioSource = GetComponent<AudioSource>();

            PieMenuSharedReferences references = PieMenuShared.References;
            infoPanelHandler = references.InfoPanelSettingsHandler;
            audioHandler = references.AudioSettingsHandler;
            pieMenuToggler = references.PieMenuToggler;

            PieMenu.OnComponentsInitialized -= GetPieMenuItemComponents;
        }

        private void InitializePlaymodeComponents()
        {
            if (Application.isPlaying)
            {
                animator = GetComponent<Animator>();
                clickHandler = GetComponent<IMenuItemClickHandler>();
            }
        }
    }
}
