using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    public class MenuItemNameChanger : MonoBehaviour
    {
        public void Change(Dictionary<int, PieMenuItem> menuItems)
        {
            string name = "Menu Item ";
            int iteration = 0;

            foreach (KeyValuePair<int, PieMenuItem> item in menuItems)
            {
                item.Value.transform.name = name + iteration;
                iteration++;
            }
        }
    }
}
