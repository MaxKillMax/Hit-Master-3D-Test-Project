using UnityEngine;

namespace HitMaster3DTestProject
{
    public abstract class ServicesInitializator : MonoBehaviour
    {
        private void Awake()
        {
            InitializeServices();
            Destroy(gameObject);
        }

        protected abstract void InitializeServices();
    }
}
