using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimplePieMenu
{
    [CustomPropertyDrawer(typeof(OnValueChangeAttribute))]
    public class OnValueChangeAttributeDrawer : PropertyDrawer
    {
        private Action valueChangeAction;
        private int previousIntValue;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();

                if (valueChangeAction == null)
                {
                    var targetObject = property.serializedObject.targetObject;
                    Type type = targetObject.GetType();
                    MethodInfo methodInfo = FindMethodInfo(type);
                    InitializeValueChangeAction(property, targetObject, methodInfo);
                }

                HandleValueChangedAction(property);
            }
        }

        private MethodInfo FindMethodInfo(Type type)
        {
            OnValueChangeAttribute onValueChangeAttribute = (OnValueChangeAttribute)attribute;
            MethodInfo methodInfo = type.GetMethod(onValueChangeAttribute.methodName);
            return methodInfo;
        }

        private void InitializeValueChangeAction(SerializedProperty property, UnityEngine.Object targetObject,
            MethodInfo methodInfo)
        {
            if (methodInfo != null)
            {
                valueChangeAction = (Action)Delegate.CreateDelegate(typeof(Action), targetObject, methodInfo);

                if (property.propertyType == SerializedPropertyType.Integer)
                {
                    previousIntValue = property.intValue;
                }
            }
        }

        private void HandleValueChangedAction(SerializedProperty property)
        {
            if (valueChangeAction != null)
            {
                if (property.propertyType == SerializedPropertyType.Integer)
                {
                    HandleIntValueChangedAction(property);
                }
                else
                {
                    InvokeActionSafely(valueChangeAction);
                }
            }
        }

        private void HandleIntValueChangedAction(SerializedProperty property)
        {
            // This code snippet ensures that when the value of a serialized field changes,
            // the method will be invoked only for integer values.

            int newIntValue = property.intValue;
            if (newIntValue != previousIntValue)
            {
                InvokeActionSafely(valueChangeAction);
            }

            previousIntValue = newIntValue;
        }

        private void InvokeActionSafely(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"StackTrace: {e.StackTrace}");
            }
        }
    }
}
