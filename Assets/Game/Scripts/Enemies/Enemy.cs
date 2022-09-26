using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Enemy : LiveObject
    {
        [SerializeField] private EnemyDestroying _enemyDestroying;

        [SerializeField] private float _startHealth;

        private void Awake()
        {
            Health = _startHealth;
        }

        public override void Destroy()
        {
            base.Destroy();
            _enemyDestroying.ActivateRagdoll();
        }
    }
}
