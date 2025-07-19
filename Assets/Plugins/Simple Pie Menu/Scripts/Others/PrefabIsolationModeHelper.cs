using UnityEngine;

namespace SimplePieMenu
{
    public class PrefabIsolationModeHelper : MonoBehaviour
    {
        public static bool IsInPrefabIsolationMode()
        {
#if UNITY_EDITOR
            if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
                return true;
#endif

            return false;
        }
    }
}
