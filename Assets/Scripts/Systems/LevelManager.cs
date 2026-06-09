using System.Collections.Generic;
using Core;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PuzzleLevelData[] levels;
        [SerializeField] private Image background;

        [Header("Prefabs")] 
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject piecePrefab;

        [Header("Parents")] 
        [SerializeField] private RectTransform referenceBoard;
        [SerializeField] private RectTransform slotsParent;
        [SerializeField] private RectTransform piecesParent;

        public static LevelManager Instance;

        public List<PuzzleSlot> AllSlots { get; } = new();

        private int _currentLevel;

        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            LoadLevel(0);
        }

        private void LoadLevel(int index)
        {
            Clear();

            _currentLevel = index;
            var data = levels[_currentLevel];

            background.sprite = data.background;

            SpawnFromReference(data);
            SpawnPieces(data);

            GameManager.Instance.Initialize(data.pieces.Length);
        }

        public void LoadNextLevel()
        {
            _currentLevel++;

            if (_currentLevel >= levels.Length)
            {
                _currentLevel = 0;
            }

            LoadLevel(_currentLevel);
        }

        private void SpawnFromReference(PuzzleLevelData data)
        {
            foreach (Transform child in referenceBoard)
            {
                var refSlot = child.GetComponent<PuzzleRefSlot>();
                if (refSlot == null) continue;

                var slotObj = Instantiate(slotPrefab, slotsParent);

                var rt = slotObj.GetComponent<RectTransform>();
                var refRt = child.GetComponent<RectTransform>();

                rt.anchoredPosition = refRt.anchoredPosition;

                var slot = slotObj.GetComponent<PuzzleSlot>();
                slot.SlotId = refSlot.id;
                slot.SetSprite(data.pieces[refSlot.id].puzzleSprite);

                AllSlots.Add(slot);
            }
        }

        private void SpawnPieces(PuzzleLevelData data)
        {
            foreach (var piece in data.pieces)
            {
                var obj = Instantiate(piecePrefab, piecesParent);

                obj.GetComponent<PuzzlePiece>().PieceId = piece.id;
                obj.GetComponent<PuzzlePiece>().SetSprite(piece.puzzleSprite);

                var rt = obj.GetComponent<RectTransform>();

                rt.anchoredPosition = new Vector2(
                    Random.Range(-300, 300),
                    Random.Range(-400, -200)
                );
            }
        }

        private void Clear()
        {
            foreach (Transform t in slotsParent) Destroy(t.gameObject);
            foreach (Transform t in piecesParent) Destroy(t.gameObject);

            AllSlots.Clear();
        }
    }
}