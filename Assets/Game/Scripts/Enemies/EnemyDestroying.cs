using UnityEngine;

namespace HitMaster3DTestProject
{
    public class EnemyDestroying : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _rigidbodies;

        [ContextMenu("Activate Ragdoll")]
        public void ActivateRagdoll()
        {
            _animator.enabled = false;

            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].velocity = Vector3.zero;
            }
        }
    }
}
