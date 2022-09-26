using System.Collections.Generic;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolSize;

        protected List<T> AvailableObjects;
        protected List<T> UsedObjects;

        private void Awake()
        {
            InitializePool();
        }
        
        protected virtual void InitializePool()
        {
            AvailableObjects = new List<T>(_poolSize);
            UsedObjects = new List<T>(_poolSize);

            for (int i = 0; i < _poolSize; i++)
                InitializePoolObject();
        }

        protected virtual void InitializePoolObject()
        {
            T t = Instantiate(_prefab);
            t.gameObject.SetActive(false);

            AvailableObjects.Add(t);
        }

        public virtual T Get()
        {
            if (AvailableObjects.Count == 0)
                return default;

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
