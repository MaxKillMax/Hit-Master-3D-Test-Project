using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class ProjectilePool : ObjectPool<Projectile>
    {
        protected Func<Transform, InteractType, bool> InteractCondition;
        protected ProjectileData ProjectileData;

        public ProjectilePool(ProjectileData projectileData, int poolSize, Func<Transform, InteractType, bool> interactCondition) : base (projectileData.Prefab, poolSize)
        {
            InteractCondition = interactCondition;
            ProjectileData = projectileData;

            InitializeProjectiles();
        }

        protected void InitializeProjectiles()
        {
            for (int i = 0; i < AvailableObjects.Count; i++)
                InitializeProjectile(AvailableObjects[i]);
        }
        
        protected void InitializeProjectile(Projectile projectile)
        {
            projectile.SetInteractCondition(InteractCondition);
            projectile.SetData(ProjectileData);
        }
    }
}
