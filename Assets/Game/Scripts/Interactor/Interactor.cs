using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Interactor : MonoBehaviour
    {
        public event Action OnObjectEnter;
        public event Action OnObjectStay;
        public event Action OnObjectExit;

        private event Func<Transform, InteractType, bool> InteractCondition;

        public void AddInteractCondition(Func<Transform, InteractType, bool> interactCondition)
        {
            InteractCondition += interactCondition;
        }

        public void RemoveInteractCondition(Func<Transform, InteractType, bool> interactCondition)
        {
            InteractCondition -= interactCondition;
        }

        protected bool CheckInteractCondition(Transform transform, InteractType interactType)
        {
            if (InteractCondition is null)
                Debug.LogWarning("Interact condition is null!");
            else if (!InteractCondition.Invoke(transform, interactType))
                return false;

            return true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectEnter?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectEnter?.Invoke();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectStay?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectStay?.Invoke();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (CheckInteractCondition(collision.transform, InteractType.Collision))
                OnObjectExit?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckInteractCondition(other.transform, InteractType.Trigger))
                OnObjectExit?.Invoke();
        }
    }
}
