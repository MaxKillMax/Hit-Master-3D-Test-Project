using System.Collections;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class EnemyDestroying : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _rigidbodies;

        private float _sleepTime = 0.35f;

        [ContextMenu("Activate Ragdoll")]
        public void ActivateRagdoll()
        {
            _animator.enabled = false;

            StartCoroutine(WaitForSleepTime());
        }

        private IEnumerator WaitForSleepTime()
        {
            while (_sleepTime > 0)
            {
                _sleepTime -= Time.deltaTime;
                Freeze();

                yield return new WaitForEndOfFrame();
            }
        }

        private void Freeze()
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
                _rigidbodies[i].velocity *= 0;
        }
    }
}
