using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public abstract class LiveObject : MonoBehaviour
    {
        public event Action<LiveObject> OnObjectDestroyed;
        public event Action<LiveObject> OnObjectDamaged;

        private float _health = 0;
        public float Health { get => _health; protected set => _health = value; }

        public void GetDamage(float value)
        {
            _health -= value;

            if (_health <= 0)
                Destroy();

            OnObjectDamaged?.Invoke(this);
        }

        public virtual void Destroy()
        {
            _health = 0;
            OnObjectDestroyed?.Invoke(this);
        }
    }
}
