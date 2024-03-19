using UnityEditor;

public class LockInspector
{
    [MenuItem("Project/Lock Inspector %l")]
    public static void Lock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }

    [MenuItem("Project/Lock Inspector %l", true)]
    public static bool Valid()
    {
        return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
    }
}