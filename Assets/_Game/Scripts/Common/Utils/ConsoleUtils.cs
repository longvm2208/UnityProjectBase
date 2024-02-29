using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ConsoleUtils
{
    public static void ClearLog()
    {
#if UNITY_EDITOR
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
#endif
    }

    public static void Log(Color color, string log)
    {
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
            (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), log));
    }
}
