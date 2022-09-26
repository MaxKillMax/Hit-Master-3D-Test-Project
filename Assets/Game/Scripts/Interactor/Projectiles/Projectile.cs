using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : Interactor
    {
        public event Action<Projectile> OnLifeTimeOut;

        [SerializeField] private Rigidbody _rigidbody;

        private ProjectileData _data;

        public float Speed => _data.Speed;
        public float Damage => _data.Damage;
        public float LifeTime => _data.LifeTime;

        private float _currentTime;

        private Vector3 _origin;
        private Vector3 _direction;

        private void Awake()
        {
            enabled = false;
        }

        public void SetData(ProjectileData data)
        {
            _data = data;
        }

        public void LaunchProjectile(Vector3 origin, Vector3 direction)
        {
            _currentTime = LifeTime;

            _origin = origin;
            _direction = direction;

            transform.position = origin;

            enabled = true;
        }

        public void StopProjectile()
        {
            enabled = false;
            _rigidbody.velocity = Vector3.zero;
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * Speed;

            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
                OnLifeTimeOut?.Invoke(this);
        }
    }
}
