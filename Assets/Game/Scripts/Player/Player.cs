using System;

namespace HitMaster3DTestProject
{
    public class Player : LiveObject
    {
        public event Action<PlayerState> OnStateChanged;

        private PlayerState _state = PlayerState.Idle;
        public PlayerState State => _state;

        public void SetState(PlayerState state)
        {
            if (_state == state)
                return;

            _state = state;
            OnStateChanged?.Invoke(_state);
        }

        public override void Destroy()
        {
            SetState(PlayerState.Dies);
            base.Destroy();
            gameObject.SetActive(false);
        }
    }
}
