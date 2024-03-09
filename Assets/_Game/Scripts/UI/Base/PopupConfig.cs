using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Sirenix.OdinInspector;

[System.Serializable]
public struct PopupConfig
{
    [SerializeField, HorizontalGroup("row")]
    private PopupId id;
    [SerializeField] private AssetReferenceGameObject reference;

    public PopupId Id => id;
    public AssetReferenceGameObject Reference => reference;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(PopupConfig))]
public class PopupConfigDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Calculate rects for variable name, min, and max fields
        Rect variableNameRect = new Rect(position.x, position.y, position.width * 0.3f, EditorGUIUtility.singleLineHeight);
        Rect idRect = new Rect(position.x + position.width * 0.3f, position.y, position.width * 0.34f, EditorGUIUtility.singleLineHeight);
        Rect referenceRect = new Rect(position.x + position.width * 0.65f, position.y, position.width * 0.35f, EditorGUIUtility.singleLineHeight);

        // Draw variable name label
        EditorGUI.LabelField(variableNameRect, label);

        // Draw min and max fields using PropertyField
        EditorGUIUtility.labelWidth = 50f; // Optional: Custom label width
        EditorGUI.PropertyField(idRect, property.FindPropertyRelative("id"), new GUIContent("Id"));
        EditorGUI.PropertyField(referenceRect, property.FindPropertyRelative("reference"), new GUIContent("Reference"));

        EditorGUI.EndProperty();
    }

    // Override GetPropertyHeight to account for single line height
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif
