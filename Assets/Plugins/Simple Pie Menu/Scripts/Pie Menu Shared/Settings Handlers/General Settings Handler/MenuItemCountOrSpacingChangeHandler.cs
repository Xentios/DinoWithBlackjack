using UnityEngine;

namespace SimplePieMenu
{
    [RequireComponent(typeof(MenuItemsCreator))]
    [RequireComponent(typeof(MenuItemsRemover))]
    [RequireComponent(typeof(FillAmountCalculator))]
    [ExecuteInEditMode]
    public class MenuItemCountOrSpacingChangeHandler : MonoBehaviour
    {
        private MenuItemsCreator menuItemsCreator;
        private MenuItemsRemover menuItemsRemover;
        private FillAmountCalculator fillAmountCalculator;

        private void OnEnable()
        {
            menuItemsCreator = GetComponent<MenuItemsCreator>();
            menuItemsRemover = GetComponent<MenuItemsRemover>();
            fillAmountCalculator = GetComponent<FillAmountCalculator>();
        }

        public void HandleButtonCountChange(PieMenu pieMenu, int newMenuItemCount)
        {
            Transform menuItemsDir = pieMenu.PieMenuElements.MenuItemsDir;
            int currentMenuItemsCount = menuItemsDir.childCount;

            if (currentMenuItemsCount > newMenuItemCount)
            {
                menuItemsRemover.Remove(pieMenu, newMenuItemCount);
            }
            else if (currentMenuItemsCount < newMenuItemCount)
            {
                menuItemsCreator.Create(pieMenu, newMenuItemCount);
            }
        }

        public void UpdateFillAmount(PieMenu pieMenu, int menuItemCount, int menuItemSpacing)
        {

            float fillAmount = fillAmountCalculator.CalculateMenuItemFillAmount(menuItemCount, menuItemSpacing);

            pieMenu.PieMenuInfo.SetFillAmount(fillAmount);
            fillAmountCalculator.ModifyFillAmount(pieMenu.GetMenuItems(), fillAmount, menuItemSpacing);
        }
    }
}
