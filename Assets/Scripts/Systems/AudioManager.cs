using UnityEngine;

namespace Systems
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource sfxSource;

        [SerializeField] private AudioClip piecePlacedClip;
        [SerializeField] private AudioClip levelCompleteClip;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayPiecePlaced()
        {
            sfxSource.PlayOneShot(piecePlacedClip);
        }

        public void PlayLevelComplete()
        {
            sfxSource.PlayOneShot(levelCompleteClip);
        }
    }
}