using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer {

  public override void OnGUI (
    Rect position,
    SerializedProperty prop,
    GUIContent label
  ) {
    string valueString;
    switch (prop.propertyType) {
      case SerializedPropertyType.Boolean:
        valueString = prop.boolValue.ToString();
        break;
      case SerializedPropertyType.Integer:
        valueString = prop.intValue.ToString();
        break;
      case SerializedPropertyType.Float:
        valueString = prop.floatValue.ToString();
        break;
      case SerializedPropertyType.String:
        valueString = prop.stringValue;
        break;
      default:
        valueString = "( Not Supported )";
        break;
    }

    EditorGUI.LabelField(position, label.text, valueString);
  }

}
#endif