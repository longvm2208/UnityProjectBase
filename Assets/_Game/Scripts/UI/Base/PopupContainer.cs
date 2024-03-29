using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Panel Popup/Scriptable Objects/Popup Container")]
public class PopupContainer : ScriptableObject
{
    [System.Serializable]
    public struct PopupConfig
    {
        [HorizontalGroup("row", Width = 0.3f), HideLabel]
        public PopupId id;
        [HorizontalGroup("row", Width = 0.7f), HideLabel]
        public AssetReferenceGameObject reference;
    }

    [SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 10)]
    private PopupConfig[] configs;

    private Dictionary<PopupId, AssetReferenceGameObject> referenceById;

    public AssetReferenceGameObject this[PopupId id]
    {
        get
        {
            if (configs == null || configs.Length == 0)
            {
                Debug.LogError("Container is empty");
                return null;
            }

            if (referenceById == null)
            {
                referenceById = new Dictionary<PopupId, AssetReferenceGameObject>();
            }

            if (referenceById.Count == 0)
            {
                for (int i = 0; i < configs.Length; i++)
                {
                    referenceById.Add(configs[i].id, configs[i].reference);
                }
            }

            if (!referenceById.ContainsKey(id))
            {
                Debug.LogError($"There is no reference corresponding to this id: {id}");
                return null;
            }

            return referenceById[id];
        }
    }
}
