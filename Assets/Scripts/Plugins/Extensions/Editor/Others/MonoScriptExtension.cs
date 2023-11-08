using UnityEditor;
using UnityEngine;

namespace Plugins.Extensions
{
    [InitializeOnLoad]
    public static class ScriptableExtension
    {
        static ScriptableExtension()
        {
            EditorApplication.contextualPropertyMenu -= OnPropertyContextMenu;
            EditorApplication.contextualPropertyMenu += OnPropertyContextMenu;
        }

        private static void OnPropertyContextMenu(GenericMenu menu, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return;

            Object obj = property.objectReferenceValue;
            if (obj == null)
            {
                return;
            }
            else if (obj is ScriptableObject scriptable)
            {
                menu.AddItem(new GUIContent("Edit"), false, () => OpenScriptableObject(scriptable));
            }
            else if (obj is MonoBehaviour monoBehaviour)
            {
                menu.AddItem(new GUIContent("Edit"), false, () => OpenMonoBehaviour(monoBehaviour));
            }

            void OpenScriptableObject(ScriptableObject scr)
            {
                AssetDatabase.OpenAsset(MonoScript.FromScriptableObject(scr));
            }

            void OpenMonoBehaviour(MonoBehaviour mono)
            {
                AssetDatabase.OpenAsset(MonoScript.FromMonoBehaviour(mono));
            }
        }
    }
}

