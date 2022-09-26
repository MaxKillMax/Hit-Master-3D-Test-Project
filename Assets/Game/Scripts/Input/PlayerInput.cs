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

        private float _lastSpeed;

        private List<Enemy> _wayPointEnemies = new List<Enemy>();

        private void Awake()
        {
            _movement.OnPlayerStopped += TryUpdateState;
        }

        private void Start()
        {
            TryUpdateState();
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
            float currentSpeed = _movement.CurrentSpeed;

            if (_lastSpeed != currentSpeed)
            {
                _lastSpeed = currentSpeed;
                _animator.SetMovementValue(currentSpeed / _movement.MaximumSpeed);
            }
        }

        private void TryAttack()
        {
            if (_player.State != PlayerState.Attacks)
                return;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                _attack.LaunchProjectile(ray);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    _attack.LaunchProjectile(ray);
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
