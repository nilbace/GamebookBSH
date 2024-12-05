using DefaultNamespace.Mobile;
using UnityEditor;
using UnityEngine;

namespace Edit.Drawer
{
    [CustomPropertyDrawer(typeof(EndDataContainerSO.EndingData))]
    public class EndingDataDrawer : PropertyDrawer
    {
      
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var editorWidth = position.width;


            SerializedProperty endingName = property.FindPropertyRelative("endingName");
            var nameRect = new Rect(position.x, position.y, editorWidth/2, position.height);
            endingName.stringValue = EditorGUI.TextField(nameRect, endingName.stringValue);
            var videoRect = new Rect(position.x + editorWidth/2, position.y, editorWidth/2, position.height);
            EditorGUI.ObjectField(videoRect, property.FindPropertyRelative("videoClip"), GUIContent.none);
            EditorGUI.indentLevel = indent;
        
            EditorGUI.EndProperty();
        }
    
        public ScriptableObject GetVariable(SerializedProperty property)
        {
            return property.objectReferenceValue as ScriptableObject;
        }
        

    }
}