using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace HitMaster3DTestProject
{
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnPlayerStopped;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform[] _points;

        private readonly float _rotateTime = 0.4f;

        private int _currentPoint = -1;
        public int CurrentPoint => _currentPoint;

        public float MaximumSpeed => _agent.speed;
        public float CurrentSpeed => _agent.velocity.x + _agent.velocity.y + _agent.velocity.z;

        public void MoveToNextPoint()
        {
            _currentPoint++;
            _agent.SetDestination(_points[_currentPoint].transform.position);
            StartCoroutine(WaitForStop());
        }

        private IEnumerator WaitForStop()
        {
            do
                yield return new WaitForEndOfFrame();
            while (_agent.remainingDistance > _agent.stoppingDistance);

            transform.DORotateQuaternion(_points[_currentPoint].rotation, _rotateTime);
            yield return new WaitForSeconds(_rotateTime);

            OnPlayerStopped?.Invoke();
        }
    }
}
