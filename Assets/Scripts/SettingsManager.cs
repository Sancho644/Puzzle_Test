using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private PuzzlePieceSettings settings;

    public static SettingsManager Instance;

    public PuzzlePieceSettings Settings => settings;

    private void Awake()
    {
        Instance = this;
    }
}