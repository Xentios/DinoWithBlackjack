using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [CustomEditor(typeof(PieMenuItem))]
    public class PieMenuItemEditor : Editor
    {
        PrefabStage prefabIsolationMode;

        private PieMenuItem menuItem;
        private GameObject parent;
        private bool selected;
        private ColorBlock colors;
        private Color preservedColor;

        private void OnEnable()
        {
            prefabIsolationMode = PrefabStageUtility.GetCurrentPrefabStage();

            menuItem = (PieMenuItem)target;

            if (menuItem.transform.parent != null)
            {
                parent = menuItem.transform.parent.gameObject;
            }

            PieMenuShared.OnBeforeSingletonDestroy += OnBeforeSingletonDestroy;
            EditorApplication.playModeStateChanged += ModeStateChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        }

        private void OnDisable()
        {
            UnselectMenuItem();

            PieMenuShared.OnBeforeSingletonDestroy -= OnBeforeSingletonDestroy;
            EditorApplication.playModeStateChanged -= ModeStateChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
        }


        private void OnBeforeSingletonDestroy()
        {
            //Debug.Log("OnBewewewforeSingletonDestroy");
            UnselectInHierarchy();
            UnselectMenuItem();
        }

        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            ManageDetailsSection();

            if (prefabIsolationMode != null || parent != null && !parent.activeSelf) return;

            if (!selected && menuItem.PieMenu != null && menuItem.PieMenu.MenuItemsTracker.ButtonComponents != null)
            {
                SelectMenuItem();
            }
        }

        private void ManageDetailsSection()
        {
            EditorGUILayout.LabelField("Details");

            EditorGUI.BeginChangeCheck();
            string newDetails = EditorGUILayout.TextArea(menuItem.Details, GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                menuItem.SetDetails(newDetails);
                menuItem.DisplayDetails();
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void SelectMenuItem()
        {
            selected = true;

            ChangeColor();
            menuItem.DisplayHeader();
            menuItem.DisplayDetails();
        }

        private void UnselectMenuItem()
        {
            if (selected)
            {
                selected = false;

                RestoreDefaultColor();

                PieMenuShared.References.InfoPanelSettingsHandler.RestoreDefaultInfoPanelText(menuItem.PieMenu);
            }
        }

        private void ChangeColor()
        {
            Button button = GetButton();

            colors = button.colors;

            preservedColor = colors.normalColor;
            colors.normalColor = colors.selectedColor;

            button.colors = colors;
        }

        private void RestoreDefaultColor()
        {
            colors.normalColor = preservedColor;

            Button button = GetButton();
            button.colors = colors;
        }

        private void OnBeforeAssemblyReload()
        {
            UnselectInHierarchy();
            UnselectMenuItem();
        }

        private void UnselectInHierarchy()
        {
            GameObject selectedObject = Selection.activeObject as GameObject;
            if (selectedObject != null)
            {
                string name = selectedObject.name;
                if (name.Contains("Menu Item"))
                {
                    Selection.activeObject = null;
                }
            }
        }

        private void ModeStateChanged(PlayModeStateChange state)
        {
            //The method unselects the menu item when exiting the edit / play mode

            if (selected &&
                (state == PlayModeStateChange.ExitingPlayMode)) //|| state == PlayddddModeStateChange.ExitingPlayMode))
            {
                UnselectInHierarchy();
                UnselectMenuItem();
            }
        }

        private Button GetButton()
        {
            PieMenuItem pieMenuItem = menuItem.PieMenu.MenuItemsTracker.GetMenuItem(menuItem.Id);

            Button button = pieMenuItem.transform.GetComponent<Button>();
            return button;
        }
    }
}
