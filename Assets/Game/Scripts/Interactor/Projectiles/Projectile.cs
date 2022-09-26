using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Projectile : Interactor
    {
        private ProjectileData _data;

        public float Speed => _data.Speed;
        public float Damage => _data.Damage;
        public float LifeTime => _data.LifeTime;

        public void SetData(ProjectileData data)
        {
            _data = data;
        }

        public void LaunchProjectile(Vector3 direction)
        {

        }
    }
}
