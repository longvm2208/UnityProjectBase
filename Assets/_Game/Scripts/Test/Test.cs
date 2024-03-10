using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Bullet bulletPrefab;

    [Button]
    public void Spawn()
    {
        bulletPrefab.Spawn();
    }
}
