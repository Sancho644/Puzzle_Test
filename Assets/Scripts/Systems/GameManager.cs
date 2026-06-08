using System.Collections;
using UnityEngine;

namespace Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject winPanel;

        public static GameManager Instance;

        private int _placedPieces;
        private int _totalPieces;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            winPanel.SetActive(false);
        }

        public void Initialize(int piecesCount)
        {
            _placedPieces = 0;
            _totalPieces = piecesCount;
        }

        public void RegisterPlacedPiece()
        {
            _placedPieces++;

            if (_placedPieces >= _totalPieces)
            {
                LevelCompleted();
            }
        }

        private void LevelCompleted()
        {
            StartCoroutine(CompleteRoutine());
        }

        private IEnumerator CompleteRoutine()
        {
            AudioManager.Instance.PlayLevelComplete();

            winPanel.SetActive(true);

            yield return new WaitForSeconds(2f);

            winPanel.SetActive(false);
            levelManager.LoadNextLevel();
        }
    }
}