using UnityEngine;

namespace HitMaster3DTestProject
{
    public class TapToPlay : MonoBehaviour
    {
        [SerializeField] private GameObject _tapToPlayUI;

        [SerializeField] private PlayerInput _playerInput;

        private void Start()
        {
            _playerInput.enabled = false;
        }

        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (Input.anyKeyDown)
                HideTapToPlayUI();
#else
            if (Input.touchCount > 0)
                HideTapToPlayUI();
#endif
        }

        private void HideTapToPlayUI()
        {
            enabled = false;
            _playerInput.ActivateInput();
            _tapToPlayUI.SetActive(false);
        }

        private void ShowTapToPlayUI()
        {
            enabled = true;
            _playerInput.DeactivateInput();
            _tapToPlayUI.SetActive(true);
        }
    }
}
