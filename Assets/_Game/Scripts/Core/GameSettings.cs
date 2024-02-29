using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    public bool isEnableAds = true;
    public bool isPlayTest = false;
}