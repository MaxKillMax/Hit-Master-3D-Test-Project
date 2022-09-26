using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Enemies : MonoBehaviour
    {
        private List<Enemy> _enemies;

        public List<Enemy> GetEnemies() => _enemies;

        private void Awake()
        {
            _enemies = FindObjectsOfType<Enemy>().ToList();

            foreach (Enemy enemy in _enemies)
                enemy.OnObjectDestroyed += RemoveEnemy;
        }

        private void RemoveEnemy(LiveObject liveObject)
        {
            liveObject.OnObjectDestroyed -= RemoveEnemy;
            _enemies.Remove((Enemy)liveObject);
        }
    }
}
