using System.Collections.Generic;
using UnityEngine;

namespace SimplePieMenu
{
    [ExecuteInEditMode]
    public class PieMenuShapeSettingsHandler : MonoBehaviour
    {
        [SerializeField] List<Sprite> shapes;

        public List<Sprite> Shapes
        {
            get { return shapes; }
        }

        public void HandleShapeChange(PieMenu pieMenu, int shapeIndex)
        {
            foreach (KeyValuePair<int, PieMenuItem> menuItem in pieMenu.GetMenuItems())
            {
                ChangeShape(menuItem.Value.transform, shapeIndex);
            }
        }

        private void ChangeShape(Transform menuItem, int shapeIndex)
        {
            ImageFilledClickableSlices menuItemImage = menuItem.GetComponent<ImageFilledClickableSlices>();
            menuItemImage.sprite = Shapes[shapeIndex];
        }
    }
}
