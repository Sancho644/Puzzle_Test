using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PuzzleSlot : MonoBehaviour
    {
        [SerializeField] private Image image;

        public int SlotId;

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}