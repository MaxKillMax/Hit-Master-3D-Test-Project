using UnityEngine;

namespace HitMaster3DTestProject
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _movementFloat;

        public void SetMovementValue(float value)
        {
            _animator.SetFloat(_movementFloat, value);
        }
    }
}
