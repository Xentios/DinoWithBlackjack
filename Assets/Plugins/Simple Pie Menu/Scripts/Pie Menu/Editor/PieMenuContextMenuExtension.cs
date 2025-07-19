using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimplePieMenu
{
    public class PieMenuContextMenuExtension : MonoBehaviour
    {
        private static GameObject pieMenuCanvas;

        private static readonly string prefabName = "Pie Menu - Canvas";
        private static readonly string prefabPath = "Assets/Simple Pie Menu/Prefabs/Pie Menu - Canvas.prefab";


        [MenuItem("GameObject/UI/Simple Pie Menu")]
        public static void CreatePieMenu()
        {
            if (pieMenuCanvas == null)
            {
                // If the prefab is located at the provided path, load it directly
                GetPrefabDirectly();

                if (pieMenuCanvas == null)
                {
                    // If the prefab is not found at the provided path, search for it in the project
                    FindPrefabInProject();
                }
            }

            InstantiatePieMenu();
            ManageEventSystem();
        }

        private static void InstantiatePieMenu()
        {
            if (pieMenuCanvas != null)
            {
                pieMenuCanvas = Instantiate(pieMenuCanvas);
                pieMenuCanvas.name = prefabName;
            }
            else Debug.Log("Pie Menu prefab not found");
        }

        private static void GetPrefabDirectly()
        {
            if (prefabPath != string.Empty)
            {
                pieMenuCanvas = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            }
        }

        private static void FindPrefabInProject()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (prefab != null && prefab.name.Contains(prefabName))
                {
                    pieMenuCanvas = prefab;
                }
            }
        }

        private static void ManageEventSystem()
        {
            // Check if EventSystem already exists in the scene
            if (FindObjectOfType<EventSystem>() == null)
            {
                // If it doesn't exist, create a new EventSystem and add it to the scene
                GameObject eventSystemObject = new("EventSystem");
                eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();
            }
        }
    }
}