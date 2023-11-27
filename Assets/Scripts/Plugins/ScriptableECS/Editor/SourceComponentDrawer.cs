using UnityEngine;
using UnityEditor;

namespace Plugins.ScriptableECS
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(IComponent), true)]
    public class SourceComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty prop = property.FindPropertyRelative("component");
            if (prop != null)
            {
                prop.isExpanded = property.isExpanded;
                Rect pos = position;
                pos.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(pos, property, new GUIContent(prop.type), false);
                EditorGUI.PropertyField(position, prop, GUIContent.none, true);
            }
            else
            {
                string name = property.type;
                name = name.Replace("managedReference<", "");
                name = name.Replace(">", "");
                EditorGUI.PropertyField(position, property, new GUIContent(name), true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty prop = property.FindPropertyRelative("component");
            if (prop != null)
                return EditorGUI.GetPropertyHeight(prop);
            else
                return EditorGUI.GetPropertyHeight(property);
        }
    }
#endif
}

