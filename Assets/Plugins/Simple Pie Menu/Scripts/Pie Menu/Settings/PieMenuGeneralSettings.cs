using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuGeneralSettings : MonoBehaviour
    {
        [OnValueChange(nameof(OnCountChange))] [Range(1, 10)] [SerializeField]
        int menuItemCount;

        [OnValueChange(nameof(OnSpacingChange))] [Range(0, 100)] [SerializeField]
        int menuItemSpacing;

        [OnValueChange(nameof(OnRotationChange))] [Range(0, 360)] [SerializeField]
        int rotation;

        [OnValueChange(nameof(OnSizeChange))] [Range(250, 1000)] [SerializeField]
        int size;

        private PieMenu pieMenu;
        private PieMenuGeneralSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializeGeneralSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializeGeneralSettings;
        }


        public void OnCountChange()
        {
            RestoreRotationToDeafault();
            ValidateFields();
            settingsHandler.HandleMenuItemCountChange(pieMenu, menuItemCount, menuItemSpacing);
            rotation = RotationCalculator.CalculateNewRotation(menuItemCount, menuItemSpacing);
            settingsHandler.HandleRotationChange(pieMenu, rotation);
        }

        public void OnSpacingChange()
        {
            RestoreRotationToDeafault();
            ValidateFields();
            settingsHandler.HandleButtonSpacingChange(pieMenu, menuItemCount, menuItemSpacing);
            rotation = rotation = RotationCalculator.CalculateNewRotation(menuItemCount, menuItemSpacing);
            settingsHandler.HandleRotationChange(pieMenu, rotation);

        }

        public void OnRotationChange()
        {
            settingsHandler.HandleRotationChange(pieMenu, rotation);
        }

        public void OnSizeChange()
        {
            settingsHandler.HandleSizeChange(pieMenu, size);
        }

        private void RestoreRotationToDeafault()
        {
            rotation = 0;
            OnRotationChange();
        }

        private void InitializeGeneralSettings()
        {
            settingsHandler = PieMenuShared.References.GeneralSettingsHandler;
            size = pieMenu.PieMenuInfo.MenuItemSize;
            pieMenu.PieMenuInfo.SetSpacing(menuItemSpacing);
            MenuItemAngleCalculator.Calculate(pieMenu);

        }

        private void ValidateFields()
        {
            if (menuItemCount == 1)
            {
                menuItemSpacing = 0;
            }
        }
    }
}
