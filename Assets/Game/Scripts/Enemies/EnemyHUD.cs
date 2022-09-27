using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace HitMaster3DTestProject
{
    public class EnemyHUD : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private Image _healthImage;

        private readonly float _fillTime = 0.1f;

        private Transform _lookAtTransform;

        private void Update()
        {
            _canvas.LookAt(_lookAtTransform);
        }
        
        public void UpdateHealth(float maxHealth, float currentHealth)
        {
            _healthImage.DOFillAmount(currentHealth / maxHealth, _fillTime);
        }

        public void ShowHUD(Transform lookAtTransform)
        {
            _lookAtTransform = lookAtTransform;
            _canvas.gameObject.SetActive(true);
            enabled = true;
        }

        public void HideHUD()
        {
            _canvas.gameObject.SetActive(false);
            enabled = false;
        }
    }
}
