using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
//using UnityEditor.AddressableAssets.Settings;
using UnityEditor;

public class CustomBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get => 0; }

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerPrefs.DeleteAll();

        GameSettings gameSettings = Resources.Load<GameSettings>("GameSettings");

        if (gameSettings.isEnableAds)
        {
            Debug.Log("Ads is enabled");
        }
        else
        {
            Debug.LogError("Ads is disabled");
        }

        if (gameSettings.isPlayTest)
        {
            Debug.LogError("Play test mode is enabled");
        }
        else
        {
            Debug.Log("Play test mode is disabled");
        }

        //string path = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
        //var settings = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(path);
        //if (settings.ActivePlayModeDataBuilderIndex == 2)
        //{
        //    Debug.Log("Addressables - Play Mode Script: Use Existing Build");
        //}
        //else
        //{
        //    Debug.LogError("Addressables - Play Mode Script: Use Asset Database");
        //}
    }
}
