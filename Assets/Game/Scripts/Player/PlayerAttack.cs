using System;
using System.Collections;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private ProjectileData _projectileData;

        private readonly int _poolSize = 5;
        private readonly Func<Transform, InteractType, bool> _interactCondition = (transform, interactType) => { return interactType == InteractType.Collision && transform.TryGetComponent(out Enemy enemy); };

        private ProjectilePool _projectilePool;

        private void Awake()
        {
            _projectilePool = new ProjectilePool(_projectileData, _poolSize, _interactCondition);
        }

        public void LaunchProjectile(Vector3 direction)
        {
            Projectile projectile =_projectilePool.Get();
            projectile.LaunchProjectile(direction);
            projectile.OnObjectEnter += Attack;
            StartCoroutine(WaitForProjectileRelease(projectile));
        }

        public void Attack(Interactor interactor)
        {
            Projectile projectile = interactor as Projectile;
            projectile.CheckedTransform.GetComponent<Enemy>().GetDamage(projectile.Damage);
        }

        private IEnumerator WaitForProjectileRelease(Projectile projectile)
        {
            yield return new WaitForSeconds(projectile.LifeTime);
            projectile.OnObjectEnter -= Attack;
            _projectilePool.Release(projectile);
        }
    }
}
