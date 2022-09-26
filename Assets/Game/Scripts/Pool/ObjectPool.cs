using System;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster3DTestProject
{
    [Serializable]
    public class ObjectPool<T> : UnityEngine.Object where T : MonoBehaviour
    {
        protected T Prefab;
        protected int PoolSize;

        protected List<T> AvailableObjects;
        protected List<T> UsedObjects;

        public ObjectPool(T prefab, int poolSize)
        {
            Prefab = prefab;
            PoolSize = poolSize;

            AvailableObjects = new List<T>(poolSize);
            UsedObjects = new List<T>(poolSize);

            InitializePoolObjects();
        }

        protected virtual void InitializePoolObjects()
        {
            for (int i = 0; i < PoolSize; i++)
                InitializePoolObject();
        }

        protected virtual void InitializePoolObject()
        {
            Release(Instantiate(Prefab));
        }

        public virtual T Get()
        {
            if (AvailableObjects.Count == 0)
                InitializePoolObject();

            T t = AvailableObjects[0];
            t.gameObject.SetActive(true);

            AvailableObjects.Remove(t);
            UsedObjects.Add(t);

            return t;
        }

        public virtual void Release(T t)
        {
            t.gameObject.SetActive(false);

            AvailableObjects.Add(t);
            if (UsedObjects.Contains(t))
                UsedObjects.Remove(t);
        }
    }
}
