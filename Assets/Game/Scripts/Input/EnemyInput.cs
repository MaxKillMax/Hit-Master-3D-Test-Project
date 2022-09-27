using UnityEngine;

namespace HitMaster3DTestProject
{
    public class EnemyInput : GameInput
    {
        [SerializeField] private PlayerMovement _playerMovement;

        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyHUD _enemyHUD;

        private void Awake()
        {
            _enemy.OnObjectDamaged += UpdateHealth;
            _enemy.OnObjectDestroyed += HideHUD;
            _playerMovement.OnPlayerStopped += ShowHUD;
        }

        private void OnDestroy()
        {
            _enemy.OnObjectDamaged -= UpdateHealth;
            _enemy.OnObjectDestroyed -= HideHUD;
            _playerMovement.OnPlayerStopped -= ShowHUD;
        }

        private void UpdateHealth(LiveObject liveObject)
        {
            _enemyHUD.UpdateHealth(_enemy.MaxHealth, _enemy.Health);
        }

        private void HideHUD(LiveObject liveObject)
        {
            _enemyHUD.HideHUD();
        }

        private void ShowHUD()
        {
            if (_enemy.WayPoint == _playerMovement.CurrentPoint && _enemy.Health > 0)
                _enemyHUD.ShowHUD(_playerMovement.transform);
        }
    }
}
