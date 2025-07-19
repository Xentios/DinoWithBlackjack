using UnityEngine;

namespace SimplePieMenu
{
    public class OnValueChangeAttribute : PropertyAttribute
    {
        public string methodName;

        public OnValueChangeAttribute(string methodName)
        {
            this.methodName = methodName;
        }
    }
}
