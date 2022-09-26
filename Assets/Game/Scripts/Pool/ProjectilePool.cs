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
        }

        protected override void InitializePoolObject()
        {
            base.InitializePoolObject();
            AvailableObjects[AvailableObjects.Count - 1].SetInteractCondition(InteractCondition);
            AvailableObjects[AvailableObjects.Count - 1].SetData(ProjectileData);
        }
    }
}
