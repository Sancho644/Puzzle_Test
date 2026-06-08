using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Create PuzzleLevelData", fileName = "PuzzleLevelData", order = 0)]
    public class PuzzleLevelData : ScriptableObject
    {
        public Sprite background;
        public PuzzlePieceData[] pieces;
    }
}