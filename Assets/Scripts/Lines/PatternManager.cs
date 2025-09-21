using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour
{
    [Header("Карточки выбора")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _panelParent;
    [SerializeField] private Sprite[] _lineSprites;

    [Header("Preview соединения")]
    [SerializeField] private Image _previewImage;

    [Header("Connections")]
    [SerializeField] private ConnectionSO[] _connections;

    [Header("Pattern Grid")]
    [SerializeField] private GameObject _patternGridPrefab;
    [SerializeField] private Transform _patternGridParent;

    private List<PatternCard> _currentCards = new List<PatternCard>();
    private List<PatternCard> _selectedCards = new List<PatternCard>();

    private void Start()
    {
        _previewImage.gameObject.SetActive(false);
        GenerateCards();
    }

    public void GenerateCards()
    {
        foreach (Transform child in _panelParent)
            Destroy(child.gameObject);
        _currentCards.Clear();

        // две случайные карточки
        for (int i = 0; i < 2; i++)
        {
            int randomId = Random.Range(0, _lineSprites.Length);
            GameObject cardObj = Instantiate(_cardPrefab, _panelParent);
            PatternCard card = cardObj.GetComponent<PatternCard>();
            card.Setup(_lineSprites[randomId], randomId);
            _currentCards.Add(card);
        }
    }

    public void SelectCard(PatternCard card)
    {
        if (_selectedCards.Contains(card)) return;

        _selectedCards.Add(card);
        card.SetSelected(true);

        if (_selectedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        PatternCard card1 = _selectedCards[0];
        PatternCard card2 = _selectedCards[1];

        if (card1.PatternId == card2.PatternId)
        {
            Debug.Log("Совпадение! Показываем превью и создаём PatternGrid.");

            // ищем ConnectionSO по Id
            ConnectionSO connection = System.Array.Find(_connections, c => c.patternId == card1.PatternId);
            if (connection != null)
            {
                // показать превью
                _previewImage.sprite = connection.previewSprite;
                _previewImage.gameObject.SetActive(true);

                yield return new WaitForSeconds(2f);

                _previewImage.gameObject.SetActive(false);

                // удалить карточки
                foreach (var c in _currentCards)
                    Destroy(c.gameObject);
                _currentCards.Clear();

                // создать PatternGrid
                if (_patternGridPrefab != null && _patternGridParent != null)
                {
                    GameObject gridGO = Instantiate(_patternGridPrefab, _patternGridParent);
                    PatternGrid pg = gridGO.GetComponent<PatternGrid>();
                    pg.Initialize(connection);
                }
            }
        }
        else
        {
            foreach (var c in _selectedCards)
                c.SetSelected(false);
        }

        _selectedCards.Clear();
    }
}
