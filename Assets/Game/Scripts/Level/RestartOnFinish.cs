using UnityEngine;
using UnityEngine.SceneManagement;

namespace HitMaster3DTestProject
{
    public class RestartOnFinish : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement.OnWayPointsIsOut += RestartScene;
        }

        private void OnDestroy()
        {
            _playerMovement.OnWayPointsIsOut -= RestartScene;
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
