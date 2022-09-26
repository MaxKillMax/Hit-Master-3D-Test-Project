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

        private readonly float _rotateTime = 0.15f;

        private int _currentPoint = 0;
        public int CurrentPoint => _currentPoint;

        public void MoveToNextPoint()
        {
            _currentPoint++;
            _agent.Move(_points[_currentPoint].transform.position);
            StartCoroutine(WaitForStop());
        }

        private IEnumerator WaitForStop()
        {
            while (!_agent.isStopped)
                yield return new WaitForEndOfFrame();

            transform.DORotate(_points[_currentPoint].rotation.eulerAngles, _rotateTime);
            yield return new WaitForSeconds(_rotateTime);

            OnPlayerStopped?.Invoke();
        }
    }
}
