using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling.SimplePool
{
    public static class ParticlePool
    {
        private static int DEFAULT_AMOUNT = 10;
        private static Dictionary<ParticleSystem, Particle> poolOfWhichPrefabDict = new Dictionary<ParticleSystem, Particle>();

        public static void Preload(ParticleSystem particle, int amount, Transform parent)
        {
            if (!poolOfWhichPrefabDict.ContainsKey(particle) || poolOfWhichPrefabDict[particle] == null)
            {
                poolOfWhichPrefabDict.Add(particle, new Particle(particle, amount, parent));
            }
        }

        public static void Play(ParticleSystem particle, Vector3 position, Quaternion rotation)
        {
            if (!poolOfWhichPrefabDict.ContainsKey(particle) || poolOfWhichPrefabDict[particle] == null)
            {
                poolOfWhichPrefabDict.Add(particle, new Particle(particle, DEFAULT_AMOUNT, null));
            }

            poolOfWhichPrefabDict[particle].Play(position, rotation);
        }

        public static void CollectAll()
        {
            foreach (var item in poolOfWhichPrefabDict)
            {
                item.Value.Collect();
            }
        }

        public static void ReleaseAll()
        {
            foreach (var item in poolOfWhichPrefabDict)
            {
                item.Value.Release();
            }
        }

        public class Particle
        {
            private int index;
            private ParticleSystem prefab;
            private List<ParticleSystem> pool = new List<ParticleSystem>();

            public Particle(ParticleSystem prefab, int amount, Transform parent)
            {
                this.prefab = prefab;

                for (int i = 0; i < amount; i++)
                {
                    pool.Add(Object.Instantiate(prefab, parent));
                }

                index = 0;
            }

            public void Play(Vector3 position, Quaternion rotation)
            {
                index = index + 1 >= pool.Count ? 0 : index + 1;

                if (pool[index].isPlaying)
                {
                    ParticleSystem particle = Object.Instantiate(prefab);
                    pool.Insert(index, particle);
                }

                pool[index].transform.SetPositionAndRotation(position, rotation);

                pool[index].Play();
            }

            public void Collect()
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    pool[i].Stop();
                }
            }

            public void Release()
            {
                while (pool.Count > 0)
                {
                    Object.Destroy(pool[0]);
                    pool.RemoveAt(0);
                }
            }
        }
    }
}
