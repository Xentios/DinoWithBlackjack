using UnityEngine;

namespace SimplePieMenu
{
    public class PieMenuInfo : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public bool IsTransitioning { get; private set; }
        public bool IsCloseable { get; private set; }
        public bool SelectionConstrained { get; private set; }
        public int Rotation { get; private set; }
        public float Scale { get; private set; }
        public Vector2 AnchoredPosition { get; private set; }

        public bool BackgroundEnabled { get; private set; }
        public bool InfoPanelEnabled { get; private set; }
        public bool IconsEnabled { get; private set; }

        public float MenuItemFillAmount { get; private set; }
        public float MenuItemDegrees { get; private set; }
        public int MenuItemSpacing { get; private set; }
        public int MenuItemPreservedSpacing { get; private set; } = -1;
        public int MenuItemInitialSize { get; private set; }
        public int MenuItemSize { get; private set; }

        public AnimationClip Animation { get; private set; }
        public AudioClip MouseHover { get; private set; }
        public AudioClip MouseClick { get; private set; }

        public void SetActiveState(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetTransitionState(bool isTransitioning)
        {
            IsTransitioning = isTransitioning;
        }

        public void SetCloseableState(bool isCloseable)
        {
            IsCloseable = isCloseable;
        }

        public void SetSelectionConstraintState(bool selectionConstrained)
        {
            SelectionConstrained = selectionConstrained;
        }

        public void SetRotation(int rotation)
        {
            Rotation = rotation;
        }


        public void SetScale(float scale)
        {
            Scale = scale;
        }

        public void SetAnchoredPosition(RectTransform rectTransform)
        {
            // This method adjusts the anchored position based on the differences in screen resolution in maximized and focused windows.

            Vector2 anchoredPosition = rectTransform.anchoredPosition;

            float difference = (float)Screen.width / Screen.currentResolution.width;

            anchoredPosition = new Vector2(anchoredPosition.x * difference, anchoredPosition.y * difference);

            AnchoredPosition = anchoredPosition;
        }


        public void SetBackgroundEnabled(bool backgroundEnabled)
        {
            BackgroundEnabled = backgroundEnabled;
        }

        public void SetInfoPanelEnabled(bool enabled)
        {
            InfoPanelEnabled = enabled;
        }

        public void SetIconsEnabled(bool enabled)
        {
            IconsEnabled = enabled;
        }



        public void SetFillAmount(float fillAmount)
        {
            MenuItemFillAmount = fillAmount;
        }

        public void SetMenuItemAngle(float angle)
        {
            MenuItemDegrees = angle;
        }

        public void SetSpacing(int spacing)
        {
            MenuItemSpacing = spacing;
        }

        public void SetPreservedSpacing(int spacingToPreserve)
        {
            MenuItemPreservedSpacing = spacingToPreserve;
        }

        public void SetMenuItemInitialSize(int size)
        {
            MenuItemInitialSize = size;
        }

        public void SetMenuItemSize(int size)
        {
            MenuItemSize = size;
        }



        public void SetAnimation(AnimationClip animationClip)
        {
            Animation = animationClip;
        }

        public void SetMouseHoverClip(AudioClip audioClip)
        {
            MouseHover = audioClip;
        }

        public void SetMouseClickClip(AudioClip audioClip)
        {
            MouseClick = audioClip;
        }
    }
}
