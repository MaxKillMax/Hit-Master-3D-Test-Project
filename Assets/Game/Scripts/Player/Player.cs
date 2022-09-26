using System;
using UnityEngine;

namespace HitMaster3DTestProject
{
    public class Player : MonoBehaviour
    {
        public event Action<PlayerState> OnStateChanged;
        public event Action<Player> OnPlayerDestroyed;

        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;

        private PlayerState _state = PlayerState.Idle;
        public PlayerState State => _state;

        public void SetState(PlayerState state)
        {
            if (_state == state)
                return;

            _state = state;
            OnStateChanged?.Invoke(_state);
        }

        public void Destroy()
        {
            SetState(PlayerState.Dies);
            gameObject.SetActive(false);
            OnPlayerDestroyed?.Invoke(this);
        }
    }
}
