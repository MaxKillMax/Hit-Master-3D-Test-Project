using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Interactor : MonoBehaviour
    {
        public event Action<Interactor> OnObjectEnter;
        public event Action<Interactor> OnObjectStay;
        public event Action<Interactor> OnObjectExit;

        private event Func<Transform, InteractType, bool> InteractCondition;

        private Transform _checkedTransform;
        public Transform CheckedTransform => _checkedTransform;

        public void SetInteractCondition(Func<Transform, InteractType, bool> interactCondition) => InteractCondition = interactCondition;

        public void ResetInteractCondition() => InteractCondition = default;

        protected bool CheckInteractCondition(Transform transform, InteractType interactType)
        {
            if (InteractCondition is null)
                Debug.LogWarning("Interact condition is null!");
            else if (!InteractCondition.Invoke(transform, interactType))
                return false;

            _checkedTransform = transform;
            return true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectEnter?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectEnter?.Invoke(this);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectStay?.Invoke(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectStay?.Invoke(this);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectExit?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectExit?.Invoke(this);
        }
    }
}
