using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _patternPrefab;    // ��������
    [SerializeField] private Transform _patternParent;     // ������ (PatternPanel)
    [SerializeField] private Button _generateButton;       // ������ ���������
    [SerializeField] private Sprite[] _patternSprites;     // ������� ��������
    [SerializeField] private AudioManager _audioManager;

    [Header("Settings")]
    [SerializeField] private int _patternsCount = 10;      // ������� �������� ����������

    private void Start()
    {
        _generateButton.onClick.AddListener(GeneratePatterns);
    }

    public void GeneratePatterns()
    {
        _audioManager.PlayPatternLinesSpawn();
        // �������� ������
        foreach (Transform child in _patternParent)
        {
            Destroy(child.gameObject);
        }

        // ������� �����
        for (int i = 0; i < _patternsCount; i++)
        {
            GameObject card = Instantiate(_patternPrefab, _patternParent);

            // �������� ��������� ������
            Sprite randomSprite = _patternSprites[Random.Range(0, _patternSprites.Length)];

            // ������������� �������� � Image
            card.GetComponent<Image>().sprite = randomSprite;
        }
    }
}
