using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [CustomEditor(typeof(PieMenuPositionSettings))]
    public class PieMenuPositionSettingsEditor : Editor
    {
        private PieMenuPositionSettings pieMenuPositionSettings;

        private void OnEnable()
        {
            pieMenuPositionSettings = (PieMenuPositionSettings)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Reset"))
            {
                pieMenuPositionSettings.ResetPosition();
            }           
        }
    }
}
