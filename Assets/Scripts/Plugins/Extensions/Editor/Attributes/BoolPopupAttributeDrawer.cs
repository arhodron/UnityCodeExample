using UnityEditor;
using UnityEngine;

namespace Plugins.Extensions
{
    [CustomPropertyDrawer(typeof(BoolPopupAttribute))]
    public class BoolPopupAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                BoolPopupAttribute attribute = this.attribute as BoolPopupAttribute;

                int value = property.boolValue ? 1 : 0;
                int res = EditorGUI.Popup(position, label, value, attribute.contents);
                if (value != res)
                    property.boolValue = res == 1 ? true : false;
            }
            else
            {
                GUI.Label(position, "Support only bool");
            }

            EditorGUI.EndProperty();
        }
    }
}