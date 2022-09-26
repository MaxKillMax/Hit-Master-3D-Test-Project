using UnityEngine;

namespace HitMaster3DTestProject
{
    public class EnemyPart : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        public Enemy Enemy => _enemy;
    }
}
