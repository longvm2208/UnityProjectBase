using System.Reflection;
using UnityEditor;
using UnityEngine;

public class EnableConstrainProportionsScale
{
    [MenuItem("Project/Enable Constrain Proportions Scale %e")]
    public static void Enable()
    {
        foreach (var activeEditor in ActiveEditorTracker.sharedTracker.activeEditors)
        {
            if (activeEditor.target is not Transform) continue;

            var transform = (Transform)activeEditor.target;
            var property = transform.GetType().GetProperty("constrainProportionsScale", BindingFlags.NonPublic | BindingFlags.Instance);

            if (property == null) continue;

            var value = (bool)property.GetValue(transform, null);
            property.SetValue(transform, !value, null);
        }
    }

    [MenuItem("Project/Enable Constrain Proportions Scale %e", true)]
    public static bool Valid()
    {
        return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
    }
}
