using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class MenuItemTemplate : MonoBehaviour
    {
        [SerializeField] GameObject menuItem;

        public GameObject MenuItem
        {
            get { return menuItem; }
        }

        public GameObject GetTemplate(Dictionary<int, PieMenuItem> menuItems)
        {
            GameObject template;
            if (menuItems.Count > 0)
            {
                template = menuItems[0].gameObject;
            }
            else
                template = menuItem;

            return template;
        }
    }
}
