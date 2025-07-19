using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuPositionSettings : MonoBehaviour
    {
        [Header("Left/Right")]
        [OnValueChange(nameof(OnPositionChange))] [Range(-1000, 1000)] [SerializeField]
        int horizontalPosition;

        [Header("Up/Down")]
        [OnValueChange(nameof(OnPositionChange))] [Range(1000, -1000)] [SerializeField]
        int verticalPosition;

        private PieMenu pieMenu;
        private PieMenuPositionSettingsHandler settingsHandler;

        private void OnEnable()
        {
            pieMenu = GetComponent<PieMenu>();
            pieMenu.OnComponentsInitialized += InitializePositionSettings;
        }

        private void OnDisable()
        {
            pieMenu.OnComponentsInitialized -= InitializePositionSettings;
        }

        public void OnPositionChange()
        {
            settingsHandler.Handle(pieMenu, horizontalPosition, verticalPosition);
        }

        public void ResetPosition()
        {
            if (settingsHandler != null)
            {
                horizontalPosition = 0;
                verticalPosition = 0;
                settingsHandler.Handle(pieMenu, 0, 0);
            }
        }


        private void InitializePositionSettings()
        {
            settingsHandler = PieMenuShared.References.PositionSettingsHandler;
        }
    }
}
