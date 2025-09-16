using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Звуки кнопок")]
    [SerializeField] private AudioClip _crystalSpawnClip;
    [SerializeField] private AudioClip _linesSpawnClip;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrystalSpawn()
    {
        _audioSource.PlayOneShot(_crystalSpawnClip);
    }

    public void PlayPatternLinesSpawn()
    {
        _audioSource.PlayOneShot(_linesSpawnClip);
    }
}
