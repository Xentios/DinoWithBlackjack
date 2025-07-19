using System.Collections;
using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(SelectionCalculator))]
    [RequireComponent(typeof(MenuItemSelector))]
    [RequireComponent(typeof(InputDeviceGetter))]
    public class MenuItemSelectionHandler : MonoBehaviour
    {
        [SerializeField] PieMenu pieMenu;

        public SelectionCalculator SelectionCalculator { get; private set; }
        public MenuItemSelector MenuItemSelector { get; private set; }
        public InputDeviceGetter InputDeviceGetter { get; private set; }

        public bool SelectionEnabled { get; private set; }
        public bool CanBeClicked { get; private set; }

        private int selection;
        private bool coroutineRunning;
        private Coroutine selectionCoroutine;
        private readonly float coroutineDelay = 0.1f;

        private void Awake()
        {
            InitializeComponents();
            pieMenu.OnComponentsInitialized += SaveReferencesToMenuItems;
        }

        private void Update()
        {
            if (SelectionEnabled)
            {
                if (!coroutineRunning)
                    selectionCoroutine = StartCoroutine(HandleSelection());

                DetectClick();
            }
            else
            {
                StopSelectionCoroutine();
                MenuItemSelector.UnselectPreviousMenuItem();
            }
        }

        private void StopSelectionCoroutine()
        {
            if (coroutineRunning)
            {
                StopCoroutine(selectionCoroutine);
                coroutineRunning = false;
            }
        }

        public void ToggleSelection(bool enabled)
        {
            SelectionEnabled = enabled;
            if (enabled) MenuItemSelector.ResetPreviousSelection();
        }

        public void EnableClickDetecting()
        {
            CanBeClicked = true;
        }

        public IEnumerator HandleSelection()
        {
            coroutineRunning = true;
            yield return new WaitForSeconds(coroutineDelay);

            selection = SelectionCalculator.CalculateSelection(pieMenu, InputDeviceGetter.InputDevice);

            if (selection != -1)
                MenuItemSelector.SelectMenuItem(pieMenu, selection);
            else
            {
                MenuItemSelector.UnselectPreviousMenuItem();
            }

            coroutineRunning = false;
        }

        public void DetectClick()
        {
            if (InputDeviceGetter.InputDevice.IsSelectionButtonPressed() && CanBeClicked && selection != -1)
            {
                bool menuItemDisabled = !MenuItemSelector.ButtonComponentsReference[selection].interactable;
                if (menuItemDisabled) return;

                CanBeClicked = false;
                MenuItemSelector.PieMenuItemsReference[selection].OnClick();
            }
        }

        private void SaveReferencesToMenuItems()
        {
            MenuItemsTracker tracker = pieMenu.MenuItemsTracker;
            MenuItemSelector.RegisterMenuItems(tracker.ButtonComponents, tracker.PieMenuItems);
        }

        private void InitializeComponents()
        {
            SelectionCalculator = GetComponent<SelectionCalculator>();
            MenuItemSelector = GetComponent<MenuItemSelector>();
            InputDeviceGetter = GetComponent<InputDeviceGetter>();
        }
    }
}