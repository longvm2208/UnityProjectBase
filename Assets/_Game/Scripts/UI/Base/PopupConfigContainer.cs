using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Panel Popup/Scriptable Objects/Popup Config Container")]
public class PopupConfigContainer : ScriptableObject
{
    [SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 10)]
    private PopupConfig[] popupConfigs;

    private Dictionary<PopupId, AssetReferenceGameObject> referenceById;

    public AssetReferenceGameObject this[PopupId id]
    {
        get
        {
            if (popupConfigs == null || popupConfigs.Length == 0)
            {
                Debug.LogError("Container is empty");
                return null;
            }

            if (referenceById == null || referenceById.Count == 0)
            {
                referenceById = new Dictionary<PopupId, AssetReferenceGameObject>();

                for (int i = 0; i < popupConfigs.Length; i++)
                {
                    referenceById.Add(popupConfigs[i].Id, popupConfigs[i].Reference);
                }
            }

            if (!referenceById.ContainsKey(id))
            {
                Debug.LogError("There is no reference corresponding to this id");
            }

            return referenceById[id];
        }
    }
}
