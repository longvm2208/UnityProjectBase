using System;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField]
    protected RectTransform myTransform;

    public event Action Closed;

    public virtual void Open(object args = null)
    {
        gameObject.SetActive(true);
        myTransform.SetAsLastSibling();
    }

    public virtual void Close()
    {
        Closed?.Invoke();
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
