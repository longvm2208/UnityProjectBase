using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PrefabInstancePool<Bullet> pool;

    [Button]
    public Bullet Spawn()
    {
        Bullet instance = pool.GetInstance(this);
        instance.pool = pool;
        return instance;
    }

    [Button]
    public void Despawn()
    {
        pool.Recycle(this);
    }
}
