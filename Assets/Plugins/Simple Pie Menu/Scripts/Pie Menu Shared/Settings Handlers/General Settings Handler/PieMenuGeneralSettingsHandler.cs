using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(MenuItemCountOrSpacingChangeHandler))]
    [RequireComponent(typeof(PieMenuRotationHandler))]
    [RequireComponent(typeof(PieMenuSizeHandler))]
    [RequireComponent(typeof(MenuItemNameChanger))]
    [ExecuteInEditMode]
    public class PieMenuGeneralSettingsHandler : MonoBehaviour
    {
        private MenuItemCountOrSpacingChangeHandler menuItemHandler;
        private PieMenuRotationHandler rotationHandler;
        private PieMenuSizeHandler sizeHandler;
        private MenuItemNameChanger nameChanger;

        private void OnEnable()
        {
            InitializeComponents();
        }

        public void HandleMenuItemCountChange(PieMenu pieMenu, int menuItemCount, int menuItemSpacing)
        {
            menuItemHandler.HandleButtonCountChange(pieMenu, menuItemCount);
            UpdateButtons(pieMenu, menuItemCount, menuItemSpacing);
        }

        public void HandleButtonSpacingChange(PieMenu pieMenu, int menuItemCount, int menuItemSpacing)
        {
            UpdateButtons(pieMenu, menuItemCount, menuItemSpacing);
        }

        public void HandleRotationChange(PieMenu pieMenu, int rotation)
        {
            rotationHandler.Handle(pieMenu, PieMenuShared.References.IconsSettingsHandler, rotation);
        }

        public void HandleSizeChange(PieMenu pieMenu, int size)
        {
            sizeHandler.Handle(pieMenu, size);
        }

        public void UpdateButtons(PieMenu pieMenu, int menuItemCount, int menuItemSpacing)
        {
            if (menuItemCount > 0)
            {
                pieMenu.PieMenuInfo.SetSpacing(menuItemSpacing);

                menuItemHandler.UpdateFillAmount(pieMenu, menuItemCount, menuItemSpacing);

                MenuItemAngleCalculator.Calculate(pieMenu);

                bool iconsEnabled = pieMenu.PieMenuInfo.IconsEnabled;
                PieMenuShared.References.IconsSettingsHandler.EnableHandler.HandleRotation(pieMenu, iconsEnabled);

                nameChanger.Change(pieMenu.GetMenuItems());
            }
        }

        private void InitializeComponents()
        {
            menuItemHandler = GetComponent<MenuItemCountOrSpacingChangeHandler>();
            rotationHandler = GetComponent<PieMenuRotationHandler>();
            sizeHandler = GetComponent<PieMenuSizeHandler>();
            nameChanger = GetComponent<MenuItemNameChanger>();
        }
    }
}
