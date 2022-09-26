using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyDestroyed;
        public event Action<Enemy> OnEnemyDamaged;

        [SerializeField] private float _startHealth;

        private float _health;
        public float Health => _health;

        private void Awake()
        {
            _health = _startHealth;
        }

        public void GetDamage(float value)
        {
            _health -= value;

            if (_health <= 0)
                Destroy();

            OnEnemyDamaged?.Invoke(this);
        }

        public void Destroy()
        {
            _health = 0;
            gameObject.SetActive(false);
            OnEnemyDestroyed?.Invoke(this);
        }
    }
}
