using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    private GameObject blocker;
    [SerializeField]
    private PanelBase panel;
    [SerializeField]
    private Transform popupParent;
    [SerializeField, ExposedScriptableObject]
    private PopupContainer container;

    private int popupWordLength = "popup".Length;
    private int blockCount = 0;
    private int popupOpenCount = 0;
    private Dictionary<PopupId, PopupBase> popupById;
    private List<AsyncOperationHandle<GameObject>> operationHandles;

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
        if (--blockCount < 0)
        {
            blockCount = 0;
        }

        blocker.SetActive(blockCount > 0);
    }

    public T Panel<T>() where T : PanelBase => panel as T;

    #region POPUP
    public bool HaveOpenPopup()
    {
        return popupOpenCount > 0;
    }

    public bool IsPopupInstantiated(PopupId id)
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

    public T Popup<T>() where T : PopupBase
    {
        PopupId id = GetIdFromType(typeof(T));
        return Popup<T>(id);
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

    private T Popup<T>(PopupId id) where T : PopupBase
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
    public void OpenPopup(PopupId id, object args = null)
    {
        if (popupById == null || operationHandles == null)
        {
            popupById = new Dictionary<PopupId, PopupBase>();
            operationHandles = new List<AsyncOperationHandle<GameObject>>();
        }

        popupOpenCount++;

        if (IsPopupInstantiated(id))
        {
            PopupBase popup = popupById[id];

            if (popup.IsActive())
            {
                Debug.LogWarning($"Popup {id} is opening");
                return;
            }

            popup.Open(args);
        }
        else
        {
            InstantiatePopup(id, args);
        }
    }

    private void InstantiatePopup(PopupId id, object args = null)
    {
        var reference = container[id];

        if (reference == null)
        {
            Debug.LogError($"Reference is null: {id}");
            return;
        }

        if (!reference.IsDone)
        {
            Debug.LogWarning($"Loading asset: {id}");
            return;
        }

        var operationHandle = reference.LoadAssetAsync();
        operationHandles.Add(operationHandle);

        EnableBlocker();

        operationHandle.Completed += (handle) =>
        {
            DisableBlocker();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                PopupBase popup = Instantiate(handle.Result, popupParent).GetComponent<PopupBase>();
                popup.Open(args);

                popup.Closed += () =>
                {
                    popupOpenCount--;
                };

                popupById[id] = popup;
            }
            else
            {
                Debug.LogError($"Cannot load asset: {handle.Status}");
            }
        };
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
