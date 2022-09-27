using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Enemy : LiveObject
    {
        [SerializeField] private EnemyDestroying _enemyDestroying;

        [SerializeField] private float _startHealth;
        [SerializeField] private int _wayPoint;

        public float MaxHealth => _startHealth;

        public int WayPoint => _wayPoint;

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
