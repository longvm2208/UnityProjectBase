using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct IntRange
{
    public int min;
    public int max;

    public IntRange(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public int GetRandom()
    {
        return Random.Range(min, max + 1);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(IntRange))]
public class IntRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Calculate rects for variable name, min, and max fields
        Rect variableNameRect = new Rect(position.x, position.y, position.width * 0.3f, EditorGUIUtility.singleLineHeight);
        Rect minRect = new Rect(position.x + position.width * 0.3f, position.y, position.width * 0.34f, EditorGUIUtility.singleLineHeight);
        Rect maxRect = new Rect(position.x + position.width * 0.65f, position.y, position.width * 0.35f, EditorGUIUtility.singleLineHeight);

        // Draw variable name label
        EditorGUI.LabelField(variableNameRect, label);

        // Draw min and max fields using PropertyField
        EditorGUIUtility.labelWidth = 30f; // Optional: Custom label width
        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), new GUIContent("Min"));
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), new GUIContent("Max"));

        EditorGUI.EndProperty();
    }

    // Override GetPropertyHeight to account for single line height
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif
