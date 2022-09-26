using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Enemies : MonoBehaviour
    {
        private Enemy[] _enemies;

        private void Awake()
        {
            _enemies = FindObjectsOfType<Enemy>();
        }
    }
}
