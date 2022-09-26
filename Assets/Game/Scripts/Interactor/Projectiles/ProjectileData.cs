using UnityEngine;

namespace HitMaster3DTestProject
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Projectile", order = 51)]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        public Projectile Prefab => _prefab;
        public float Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;
    }
}
