using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenu : MonoBehaviour
    {
        public PieMenuInfo PieMenuInfo { get; private set; }
        public PieMenuElements PieMenuElements { get; private set; }
        public MenuItemsTracker MenuItemsTracker { get; private set; }
        public MenuItemSelectionHandler SelectionHandler { get; private set; }
        public MenuItemTemplate MenuItemTemplate { get; private set; }

        public event System.Action OnComponentsInitialized;
        public event System.Action OnPieMenuFullyInitialized;


#if UNITY_EDITOR
        private void OnEnable()
        {
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
        }

        private void OnDisable()
        {
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
        }
#endif

        private void Start()
        {
            InitializePieMenu();
        }

        private void OnAfterAssemblyReload()
        {
            InitializePieMenu();
        }

        public Dictionary<int, PieMenuItem> GetMenuItems()
        {
            return MenuItemsTracker.PieMenuItems;
        }

        public Transform GetTemplate()
        {
            return MenuItemTemplate.GetTemplate(MenuItemsTracker.PieMenuItems).transform;
        }

        private void InitializePieMenu()
        {
            if (PrefabIsolationModeHelper.IsInPrefabIsolationMode()) return;

            InitializeComponents();
            ReadDataAndSetPieMenuInfoFields();

            OnComponentsInitialized?.Invoke();
            OnPieMenuFullyInitialized?.Invoke();

            if (gameObject.activeSelf && Application.isPlaying)
            {
                PieMenuShared.References.PieMenuToggler.SetActive(this, true);
            }
        }

        private void InitializeComponents()
        {
            PieMenuInfo = GetComponentInChildren<PieMenuInfo>();
            PieMenuElements = GetComponentInChildren<PieMenuElements>();

            MenuItemsTracker = GetComponentInChildren<MenuItemsTracker>();
            MenuItemsTracker.Initialize(PieMenuElements.MenuItemsDir);

            SelectionHandler = GetComponentInChildren<MenuItemSelectionHandler>();
            MenuItemTemplate = PieMenuShared.References.MenuItemTemplate;
        }

        private void ReadDataAndSetPieMenuInfoFields()
        {
            PieMenuInfo.SetFillAmount(GetTemplate().GetComponent<Image>().fillAmount);

            float menuItemInitialSize = MenuItemTemplate.MenuItem.GetComponent<RectTransform>().sizeDelta.x;
            PieMenuInfo.SetMenuItemInitialSize((int)menuItemInitialSize);

            float menuItemSize = GetTemplate().GetComponent<RectTransform>().sizeDelta.x;
            PieMenuInfo.SetMenuItemSize((int)menuItemSize);
            float scale = PieMenuSizeHandler.CalculatePieMenuScale(this, (int)menuItemSize);
            PieMenuInfo.SetScale(scale);

            int rotation = (int)PieMenuElements.MenuItemsDir.rotation.eulerAngles.z;
            PieMenuInfo.SetRotation(rotation);

            RectTransform rectTransform = GetComponent<RectTransform>();
            PieMenuInfo.SetAnchoredPosition(rectTransform);

        }
    }
}
