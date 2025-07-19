using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MenuItemHider))]
    [RequireComponent(typeof(MenuItemDisabler))]
    [RequireComponent(typeof(MenuItemAdder))]
    public class MenuItemsManager : MonoBehaviour
    {
        public MenuItemHider MenuItemHider { get; private set; }
        public MenuItemDisabler MenuItemDisabler { get; private set; }
        public MenuItemAdder MenuItemAdder { get; private set; }

        private void OnEnable()
        {
            MenuItemHider = GetComponent<MenuItemHider>();
            MenuItemDisabler = GetComponent<MenuItemDisabler>();
            MenuItemAdder = GetComponent<MenuItemAdder>();
        }
    }
}
