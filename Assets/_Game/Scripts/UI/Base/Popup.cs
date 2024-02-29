using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    [SerializeField]
    protected Transform myTransform;

    public virtual void Open(object obj = null)
    {
        gameObject.SetActive(true);
        myTransform.SetAsLastSibling();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.OnPopupClosed();
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
