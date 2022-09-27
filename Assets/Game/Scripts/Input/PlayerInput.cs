using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class PlayerInput : GameInput
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerAttack _attack;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private Player _player;
        [SerializeField] private Enemies _enemies;
        [SerializeField] private Camera _camera;

        private readonly float _speedMultiply = 2;
        private readonly float _speedReduction = 0.1f;
        private float _lastSpeed;

        private List<Enemy> _wayPointEnemies = new List<Enemy>();

        private void Awake()
        {
            _movement.OnPlayerStopped += TryUpdateState;
        }

        public void ActivateInput()
        {
            enabled = true;
            TryUpdateState();
        }

        public void DeactivateInput()
        {
            enabled = false;
        }

        private void OnDestroy()
        {
            _movement.OnPlayerStopped -= TryUpdateState;
        }

        private void Update()
        {
            TryUpdateAnimation();
            TryAttack();
        }

        private void TryUpdateAnimation()
        {
            if (_player.State == PlayerState.Moving)
            {
                _lastSpeed += Time.deltaTime * _movement.MaximumSpeed * _speedMultiply;
                _animator.SetMovementValue(_lastSpeed / _movement.MaximumSpeed);
            }
            else
            {
                _lastSpeed -= _speedReduction * Time.deltaTime;
                _animator.SetMovementValue(_lastSpeed / _movement.MaximumSpeed);
            }
        }

        private void SetMovementToNull()
        {
            _lastSpeed = 0;
            _animator.SetMovementValue(0);
        }

        private void TryAttack()
        {
            if (_player.State != PlayerState.Attacks)
                return;

            Ray ray;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    _attack.LaunchProjectile(raycastHit.point);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    ray = _camera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                        _attack.LaunchProjectile(raycastHit.point);
                }    
            }
#endif
        }

        private void TryUpdateState()
        {
            _wayPointEnemies = _enemies.GetEnemies().Where((enemy) => enemy.WayPoint == _movement.CurrentPoint).ToList();

            if (_wayPointEnemies.Count == 0)
            {
                _player.SetState(PlayerState.Moving);

                _movement.MoveToNextPoint();
            }
            else
            {
                _player.SetState(PlayerState.Attacks);

                SetMovementToNull();

                foreach (Enemy enemy in _wayPointEnemies)
                    enemy.OnObjectDestroyed += OnEnemyDestroyed;
            }
        }

        private void OnEnemyDestroyed(LiveObject enemy)
        {
            enemy.OnObjectDestroyed -= OnEnemyDestroyed;
            _wayPointEnemies.Remove((Enemy)enemy);

            if (_wayPointEnemies.Count == 0)
                TryUpdateState();
        }
    }
}
