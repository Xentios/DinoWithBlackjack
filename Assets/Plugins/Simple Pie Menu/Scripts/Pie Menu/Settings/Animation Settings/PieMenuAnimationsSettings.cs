using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuAnimationsSettings : MonoBehaviour
    {
        public int AnimationDropdownList { get; private set; }
        public List<string> AnimationNames { get; private set; } = new();

        private PieMenu pieMenu;
        private PieMenuAnimationsSettingsHandler animationsHandler;
        private PieMenuInfoPanelSettingsHandler infoPanelHandler;

        private Animator animator;
        private AnimationClip clip;
        private bool infoPanelEnabled;
        private double previewStartTime;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeAnimationsSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeAnimationsSettings;
        }

        public void CreateAnimationsDropdownList(int list)
        {
            AnimationDropdownList = list;
        }

        public void SwapAnimations()
        {
            animationsHandler.SwapAnimationControllers(pieMenu, AnimationDropdownList);
        }

#if UNITY_EDITOR
        public void StartPreview()
        {
            previewStartTime = EditorApplication.timeSinceStartup;
            PreparePreview();
            EditorApplication.update += DoPreview;
        }

        private void DoPreview()
        {
            double timeElapsed = EditorApplication.timeSinceStartup - previewStartTime;
            clip.SampleAnimation(animator.gameObject, (float)timeElapsed);

            if (timeElapsed >= clip.length)
            {
                EditorApplication.update -= DoPreview;

                if (infoPanelEnabled)
                {
                    infoPanelEnabled = false;
                    infoPanelHandler.SetActive(pieMenu, true);
                }
            }
        }
#endif


        private void PreparePreview()
        {
            AnimatorOverrideController overrideController = animationsHandler.GetAnimator(AnimationDropdownList);
            clip = animationsHandler.GetAnimation(overrideController);

            infoPanelEnabled = pieMenu.PieMenuInfo.InfoPanelEnabled;
            if (infoPanelEnabled)
            {
                infoPanelHandler.SetActive(pieMenu, false);
            }
        }

        private void InitializeAnimationsSettings()
        {
            animationsHandler = PieMenuShared.References.AnimationsSettingsHandler;
            infoPanelHandler = PieMenuShared.References.InfoPanelSettingsHandler;
            animator = pieMenu.PieMenuElements.Animator;

            InitializeList();
            GetSelectedAnimation();
        }

        private void InitializeList()
        {
            AnimationNames = new();
            foreach (AnimatorOverrideController overrideController in animationsHandler.AnimatorOverrideControllers)
            {
                AnimationClip animationClip = animationsHandler.GetAnimation(overrideController);

                AnimationNames.Add(animationClip.name);
            }
        }

        private void GetSelectedAnimation()
        {
            RuntimeAnimatorController runtimeController = animator.runtimeAnimatorController;
            AnimationClip animationClip = animationsHandler.GetAnimation(runtimeController);
            string currentAnimationName = animationClip.name;

            //if the List<T>.FindIndex method doesn't find a matching element, it returns -1
            int index = AnimationNames.FindIndex(name => name == currentAnimationName);

            if (index == -1)
            {
                throw new Exception("It's likely that you've added a new animation to your menu incorrectly." +
                                    " Please refer to the documentation to learn more.");
            }
            else
            {
                CreateAnimationsDropdownList(index);
                pieMenu.PieMenuInfo.SetAnimation(animationClip);
            }

        }
    }
}



