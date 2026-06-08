using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Create PuzzlePieceSettings", fileName = "PuzzlePieceSettings", order = 0)]
    public class PuzzlePieceSettings : ScriptableObject
    {
        public float snapDistance;
    }
}