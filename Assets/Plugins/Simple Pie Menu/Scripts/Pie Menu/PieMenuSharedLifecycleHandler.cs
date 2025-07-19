using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuSharedLifecycleHandler : MonoBehaviour
    {
        [SerializeField] GameObject pieMenuShared;

        private void OnEnable()
        {
            if (PrefabIsolationModeHelper.IsInPrefabIsolationMode()) return;

            if (PieMenuShared.Instance == null)
            {
                GameObject singleton = Instantiate(pieMenuShared, null);
                singleton.name = "Pie Menu Shared";    
            }

            PieMenuShared.Instance.OnNewPieMenuCreated();
        }

        private void OnDisable()
        {
            if (PieMenuShared.Instance != null)
                PieMenuShared.Instance.OnPieMenuDestroyed();
        }
    }
}
