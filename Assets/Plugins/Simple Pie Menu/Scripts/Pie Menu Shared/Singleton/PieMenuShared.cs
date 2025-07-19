using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuShared : MonoBehaviour
    {
        public static PieMenuShared Instance { get; private set; }

        public static PieMenuSharedReferences References
        {
            get { return Instance.references; }
        }

        public static event System.Action OnBeforeSingletonDestroy;

        private PieMenuSharedReferences references;
        private int pieMenuOnSceneCount;

        private void OnEnable()
        {
            if (PrefabIsolationModeHelper.IsInPrefabIsolationMode()) return;

            CreateSingleton();
        }

        private void OnApplicationQuit()
        {
            if (!Application.isPlaying)
                DestroySingleton();
        }

        private void CreateSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                references = GetComponent<PieMenuSharedReferences>();

#if UNITY_EDITOR
                EditorApplication.playModeStateChanged += ModeStateChanged;
                AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
#endif
            }
            else DestroyImmediate(this.gameObject);
            
        }

        public void OnNewPieMenuCreated()
        {
            Instance.pieMenuOnSceneCount++;
        }

        public void OnPieMenuDestroyed()
        {
            Instance.pieMenuOnSceneCount--;

            if (Instance.pieMenuOnSceneCount == 0 && !Application.isPlaying)
            {
                DestroySingleton();
            }
            
        }

#if UNITY_EDITOR
        private void ModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                DestroySingleton();
            }
        }

        private void OnBeforeAssemblyReload()
        {
            DestroySingleton();
        }
#endif
        private void DestroySingleton()
        {
            if (Instance != null)
            {
                OnBeforeSingletonDestroy?.Invoke();

                DestroyImmediate(Instance.gameObject);
                Instance = null;

#if UNITY_EDITOR
                EditorApplication.playModeStateChanged -= ModeStateChanged;
                AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
#endif
            }
        }
    }
}
