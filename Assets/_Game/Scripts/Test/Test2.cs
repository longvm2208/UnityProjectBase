using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField] private int a;

    [Button]
    public void Raise()
    {
        EventDispatcher.DispatchEvent(EventId.Example, a);
    }
}
