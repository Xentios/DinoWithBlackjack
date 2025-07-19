using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [CustomEditor(typeof(PieMenuAnimationsSettings))]
    public class PieMenuAnimationsSettingsEditor : Editor
    {
        private GUIContent animationLabel;
        private PieMenuAnimationsSettings animationsSettings;

        private void OnEnable()
        {
            animationLabel = new GUIContent("Animation:");
            animationsSettings = (PieMenuAnimationsSettings)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            animationsSettings.CreateAnimationsDropdownList(EditorGUILayout.Popup(
                animationLabel,
                animationsSettings.AnimationDropdownList,
                animationsSettings.AnimationNames.ToArray()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Show Animation"))
            {
                animationsSettings.StartPreview();
            }

            if (GUILayout.Button("Set Animation"))
            {
                animationsSettings.SwapAnimations();
            }

            GUILayout.EndHorizontal();
        }
    }
}
