using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuToggler : MonoBehaviour
    {
        private PieMenuSharedReferences references;
        private PieMenuInfoPanelSettingsHandler infoPanelHandler;
        private PieMenuAnimationsSettingsHandler animationsHandler;

        private readonly Dictionary<PieMenu, PieMenuState> pieMenuStates = new();
        private PieMenuState currentMenu;

        private int openedPieMenusCount;

        private void Update()
        {
            CloseOnReturnPressed();
        }

        public void SetActive(PieMenu pieMenu, bool isActive)
        {
            RegisterNewMenu(pieMenu);

            GetSharedComponents();

            var state = pieMenuStates[pieMenu];

            DisableInfoPanel(state);

            if (isActive)
                ShowPieMenu(state);
            else
                HidePieMenu(state);
        }

        private void RegisterNewMenu(PieMenu pieMenu)
        {
            if (!pieMenuStates.ContainsKey(pieMenu))
            {
                PieMenuState newState = new();
                newState.SetComponents(pieMenu);
                pieMenuStates[pieMenu] = newState;
            }
        }

        private void ShowPieMenu(PieMenuState state)
        {
            state.PieMenuInfo.SetTransitionState(true);
            state.PieMenuGO.SetActive(true);
            animationsHandler.PlayAnimation(state.Animator, PieMenuAnimationsSettingsHandler.TriggerActiveTrue);
            StartCoroutine(WaitForAudioAndAnimationToFinishPlaiyng(state, true));
        }

        private void HidePieMenu(PieMenuState state)
        {
            state.SelectionHandler.ToggleSelection(false);
            references.AudioSettingsHandler.PlayAudio(state.PieMenuElements.MouseClickAudioSource);
            animationsHandler.PlayAnimation(state.Animator, PieMenuAnimationsSettingsHandler.TriggerActiveFalse);
            StartCoroutine(WaitForAudioAndAnimationToFinishPlaiyng(state, false));
        }

        private IEnumerator WaitForAudioAndAnimationToFinishPlaiyng(PieMenuState state, bool isActive)
        {
            float timeToWait = CalculateTimeToWait(state.PieMenu);

            yield return new WaitForSeconds(timeToWait);

            if (isActive)
            {
                EnableInfoPanel(state);
                state.SelectionHandler.ToggleSelection(true);
                state.SelectionHandler.EnableClickDetecting();

                ManageScriptLifecycle(true, state);
            }
            else
            {
                state.PieMenuGO.SetActive(false);
                state.PieMenuInfo.SetTransitionState(false);
                ManageScriptLifecycle(false);
            }


            state.PieMenu.PieMenuInfo.SetActiveState(isActive);
        }

        private float CalculateTimeToWait(PieMenu pieMenu)
        {
            float timeToWait;

            PieMenuInfo pieMenuInfo = pieMenu.PieMenuInfo;
            float audioClipLength = pieMenuInfo.MouseClick.length;
            float animationClipLength = pieMenuInfo.Animation.length;

            timeToWait = Mathf.Max(audioClipLength, animationClipLength);
            return timeToWait;
        }

        private void ManageScriptLifecycle(bool isMenuOpening, PieMenuState state = null)
        {
            if (isMenuOpening)
            {
                currentMenu = state;
                enabled = true;
                openedPieMenusCount++;
            }
            else
            {
                openedPieMenusCount--;
                if (openedPieMenusCount == 0)
                {
                    currentMenu = null;
                    enabled = false;
                }
            }
        }

        private void CloseOnReturnPressed()
        {
            if (currentMenu != null)
            {
                bool closeable = currentMenu.PieMenuInfo.IsCloseable;
                if (closeable)
                {
                    bool returnPressed = currentMenu.SelectionHandler.InputDeviceGetter.InputDevice.IsCloseButtonPressed();

                    if (returnPressed )
                    {
                        SetActive(currentMenu.PieMenu, false);
                    }
                }
            }
        }

        private void GetSharedComponents()
        {
            if (references == null)
            {
                references = PieMenuShared.References;
                infoPanelHandler = references.InfoPanelSettingsHandler;
                animationsHandler = references.AnimationsSettingsHandler;
            }
        }

        private void DisableInfoPanel(PieMenuState state)
        {
            if (state.PieMenuInfo.InfoPanelEnabled)
            {
                infoPanelHandler.SetActive(state.PieMenu, false);
            }
        }

        private void EnableInfoPanel(PieMenuState state)
        {
            if (state.PieMenuInfo.InfoPanelEnabled)
            {
                PieMenu pieMenu = state.PieMenu;
                infoPanelHandler.SetActive(pieMenu, true);
                infoPanelHandler.ModifyHeader(pieMenu, string.Empty);
                infoPanelHandler.ModifyDetails(pieMenu, string.Empty);
            }
        }
    }

    class PieMenuState
    {
        public PieMenu PieMenu { get; private set; }
        public PieMenuInfo PieMenuInfo { get; private set; }
        public PieMenuElements PieMenuElements { get; private set; }
        public GameObject PieMenuGO { get; private set; }
        public MenuItemSelectionHandler SelectionHandler { get; private set; }
        public Animator Animator { get; private set; }

        public void SetComponents(PieMenu pieMenu)
        {
            PieMenu = pieMenu;
            PieMenuInfo = pieMenu.PieMenuInfo;
            PieMenuElements = pieMenu.PieMenuElements;
            PieMenuGO = PieMenuElements.PieMenu.gameObject;
            SelectionHandler = pieMenu.SelectionHandler;
            Animator = PieMenuElements.Animator;
        }
    }
}
