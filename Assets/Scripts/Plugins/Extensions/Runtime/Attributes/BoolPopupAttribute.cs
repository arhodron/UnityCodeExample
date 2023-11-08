using UnityEngine;

namespace Plugins.Extensions
{
    public class BoolPopupAttribute : PropertyAttribute
    {
        public GUIContent[] contents { get; private set; } = new GUIContent[] { new GUIContent("False"), new GUIContent("True") };

        public BoolPopupAttribute() { }

        public BoolPopupAttribute(params string[] names)
        {
            if (names.Length < 2)
                return;

            contents = new GUIContent[] { new GUIContent(names[0]), new GUIContent(names[1]) };
        }
    }
}


