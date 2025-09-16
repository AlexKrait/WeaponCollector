using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _patternPrefab;    // карточка
    [SerializeField] private Transform _patternParent;     // панель (PatternPanel)
    [SerializeField] private Button _generateButton;       // кнопка генерации
    [SerializeField] private Sprite[] _patternSprites;     // спрайты рисунков
    [SerializeField] private AudioManager _audioManager;

    [Header("Settings")]
    [SerializeField] private int _patternsCount = 10;      // сколько картинок показывать

    private void Start()
    {
        _generateButton.onClick.AddListener(GeneratePatterns);
    }

    public void GeneratePatterns()
    {
        _audioManager.PlayPatternLinesSpawn();
        // Очистить старые
        foreach (Transform child in _patternParent)
        {
            Destroy(child.gameObject);
        }

        // Создать новые
        for (int i = 0; i < _patternsCount; i++)
        {
            GameObject card = Instantiate(_patternPrefab, _patternParent);

            // Выбираем случайный спрайт
            Sprite randomSprite = _patternSprites[Random.Range(0, _patternSprites.Length)];

            // Устанавливаем картинку в Image
            card.GetComponent<Image>().sprite = randomSprite;
        }
    }
}
