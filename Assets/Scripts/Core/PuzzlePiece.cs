using System.Collections.Generic;
using DG.Tweening;
using Settings;
using Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float returnDuration;

        public int PieceId;
        public bool IsPlaced;

        private Vector3 _startPos;
        private Vector3 _offset;

        private List<PuzzleSlot> _allSlots;
        private PuzzlePieceSettings _settings;

        private void Start()
        {
            _allSlots = LevelManager.Instance.AllSlots;
            _settings = SettingsManager.Instance.Settings;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsPlaced) return;

            _startPos = rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsPlaced) return;

            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsPlaced) return;

            CheckPlacement();
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        private void CheckPlacement()
        {
            foreach (var slot in _allSlots)
            {
                if (slot == null)
                    continue;

                if (Vector2.Distance(transform.position, slot.transform.position) < _settings.snapDistance)
                {
                    if (slot.SlotId == PieceId)
                    {
                        Snap(slot);
                        return;
                    }
                }
            }

            ReturnBack();
        }

        private void ReturnBack()
        {
            rectTransform.DOAnchorPos(_startPos, returnDuration);
        }

        private void Snap(PuzzleSlot slot)
        {
            transform.position = slot.transform.position;

            transform.DOScale(1.1f, 0.1f).OnComplete(() => { transform.DOScale(1f, 0.1f); });

            IsPlaced = true;

            AudioManager.Instance.PlayPiecePlaced();
            GameManager.Instance.RegisterPlacedPiece();
        }
    }
}