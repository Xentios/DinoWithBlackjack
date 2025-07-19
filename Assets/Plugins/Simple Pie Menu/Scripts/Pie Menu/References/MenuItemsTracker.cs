using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemsTracker : MonoBehaviour
    {
        public Dictionary<int, Button> ButtonComponents { get; private set; } = new();
        public Dictionary<int, PieMenuItem> PieMenuItems { get; private set; } = new();

        public readonly Dictionary<int, PieMenuItem> HiddenMenuItems = new();

        public void Initialize(Transform menuItemsDir)
        {
            ButtonComponents.Clear();
            PieMenuItems.Clear();

            int childCount = menuItemsDir.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform menuItem = menuItemsDir.GetChild(i);
                InitializeMenuItem(menuItem, i);
            }
        }

        public void InitializeMenuItem(Transform menuItem, int itemIndex)
        {
            PieMenuItem pieMenuItem = menuItem.GetComponent<PieMenuItem>();
            pieMenuItem.SetId(itemIndex);
            PieMenuItems.Add(itemIndex, pieMenuItem);

            Button button = menuItem.GetComponent<Button>();
            ButtonComponents.Add(itemIndex, button);
        }

        public void RemoveMenuItem(int itemIndex)
        {
            Transform menuItem = GetMenuItem(itemIndex).transform;

            PieMenuItems.Remove(itemIndex);
            ButtonComponents.Remove(itemIndex);

            if (Application.isPlaying)
            {
                Destroy(menuItem);
            }
            else
            {
                DestroyImmediate(menuItem.gameObject);
            }
        }

        public PieMenuItem GetMenuItem(int id)
        {
            if (PieMenuItems != null && PieMenuItems.ContainsKey(id))
            {
                if (PieMenuItems[id].Id == id)
                    return PieMenuItems[id];
                else return SearchForMenuItem(id);
            }
            else
            {
                return SearchForMenuItem(id);
            }

        }

        private PieMenuItem SearchForMenuItem(int id)
        {
            // this code may be executed after using Hide Menu Item functionality, where dict index and Menu Item Id can be shifted.
            var pair = PieMenuItems.FirstOrDefault(item => item.Value.Id == id);

            if (pair.Value == null)
            {
                return null;
            }
            else return pair.Value;
        }
    }
}
