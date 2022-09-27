using UnityEngine;

namespace HitMaster3DTestProject
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField] private Transform _bulletOrigin;

        private readonly int _poolSize = 15;

        private ProjectilePool _projectilePool;

        private void Awake()
        {
            _projectilePool = new ProjectilePool(_projectileData, _poolSize, (transform, interactType) => transform.TryGetComponent(out EnemyPart enemyPart) && enemyPart.Enemy.Health > 0);
        }

        public void LaunchProjectile(Vector3 point)
        {
            Projectile projectile =_projectilePool.Get();

            Vector3 heading = point - _bulletOrigin.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            projectile.LaunchProjectile(_bulletOrigin.position, direction);

            projectile.OnObjectEnter += Attack;
            projectile.OnLifeTimeOut += ReleaseProjectile;
        }

        private void Attack(Interactor interactor)
        {
            Projectile projectile = interactor as Projectile;

            projectile.CheckedTransform.GetComponent<EnemyPart>().Enemy.GetDamage(projectile.Damage);

            ReleaseProjectile(projectile);
        }

        private void ReleaseProjectile(Projectile projectile)
        {
            projectile.OnObjectEnter -= Attack;
            projectile.OnLifeTimeOut -= ReleaseProjectile;

            projectile.StopProjectile();

            _projectilePool.Release(projectile);
        }
    }
}
