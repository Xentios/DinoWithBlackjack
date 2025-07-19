using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [CustomEditor(typeof(PieMenuShapeSettings))]
    public class PieMenuShapeSettingsEditor : Editor
    {
        private GUIContent shapeLabel;
        private PieMenuShapeSettings shapeSettings;


        private void OnEnable()
        {
            shapeLabel = new GUIContent("Shape:");
            shapeSettings = (PieMenuShapeSettings)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            int selectedShape = EditorGUILayout.Popup(shapeLabel, shapeSettings.ShapeDropdownList,
                shapeSettings.ShapeNames.ToArray());

            shapeSettings.CreateDropdownShapesList(selectedShape);

            if (EditorGUI.EndChangeCheck())
            {
                shapeSettings.OnListSelectionChange();
            }
        }
    }
}
