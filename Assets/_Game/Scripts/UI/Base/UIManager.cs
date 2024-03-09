using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Longvm;

[System.Serializable]
public class TestDictionary : SerializedDictionary<PopupId, AssetReferenceGameObject> { }

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    private Transform popupParent;
    [SerializeField]
    private GameObject blocker;
    [SerializeField]
    private Panel panel;
    [SerializeField, ExposedScriptableObject]
    private PopupConfigContainer container;
    [SerializeField]
    private TestDictionary test;

    private int popupWordLength = "popup".Length;
    private int blockCount = 0;
    private int popupOpenCount = 0;
    private Dictionary<PopupId, Popup> popupById = new();
    private List<AsyncOperationHandle<GameObject>> operationHandles = new();

    private void OnDestroy()
    {
        ReleasePopupReference();
    }

    public void EnableBlocker()
    {
        blockCount++;
        blocker.SetActive(true);
    }

    public void DisableBlocker()
    {
        if (--blockCount < 0) blockCount = 0;
        blocker.SetActive(blockCount > 0);
    }

    public T Panel<T>() where T : Panel => panel as T;

    #region POPUP
    public bool HaveOpenPopup()
    {
        return popupOpenCount > 0;
    }

    private bool IsPopupInstantiated(PopupId id)
    {
        return popupById.ContainsKey(id) && popupById[id] != null;
    }

    public bool IsPopupOpen(PopupId id)
    {
        return IsPopupInstantiated(id) && popupById[id].IsActive();
    }

    public bool IsLoadingAsset(PopupId id)
    {
        return !container[id].IsDone;
    }

    private PopupId GetIdFromType(Type type)
    {
        if (Enum.TryParse(type.Name.Remove(0, popupWordLength), out PopupId id))
        {
            return id;
        }

        Debug.LogError($"There is no id corresponding to this type: {type.Name}");
        return PopupId.None;
    }

    public T Popup<T>() where T : Popup
    {
        PopupId id = GetIdFromType(typeof(T));
        return Popup<T>(id);
    }

    private T Popup<T>(PopupId id) where T : Popup
    {
        if (id == PopupId.None) return null;

        if (IsPopupOpen(id))
        {
            return popupById[id] as T;
        }
        else
        {
            Debug.LogError($"Trying to access a closed popup: {id}");
            return null;
        }
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void OpenPopup(PopupId id, object obj = null)
    {
        popupOpenCount++;

        if (IsPopupInstantiated(id))
        {
            Popup popup = popupById[id];
            if (popup.IsActive())
            {
                Debug.LogWarning($"Popup {id} is opening");
                return;
            }
            popup.Open(obj);
        }
        else
        {
            InstantiatePopup(id, obj);
        }
    }

    private void InstantiatePopup(PopupId id, object obj = null)
    {
        var reference = container[id];

        if (reference == null)
        {
            Debug.LogError("Reference is null");
            return;
        }

        if (!reference.IsDone)
        {
            Debug.LogWarning("Loading asset");
            return;
        }

        var operationHandle = reference.LoadAssetAsync();
        operationHandles.Add(operationHandle);

        blocker.SetActive(true);

        operationHandle.Completed += (handle) =>
        {
            blocker.SetActive(false);

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Popup popup = Instantiate(handle.Result, popupParent).GetComponent<Popup>();
                popup.Open(obj);
                popupById[id] = popup;
            }
            else
            {
                Debug.LogError($"Cannot load asset: {handle.Status}");
            }
        };
    }

    public void OnPopupClosed()
    {
        popupOpenCount--;
    }

    private void ReleasePopupReference()
    {
        if (operationHandles == null || operationHandles.Count == 0) return;

        for (int i = 0; i < operationHandles.Count; i++)
        {
            Addressables.Release(operationHandles[i]);
        }
    }
    #endregion
}

