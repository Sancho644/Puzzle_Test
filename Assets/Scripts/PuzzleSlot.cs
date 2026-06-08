using UnityEngine;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour
{
    [SerializeField] private Image image;

    public int SlotId;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}