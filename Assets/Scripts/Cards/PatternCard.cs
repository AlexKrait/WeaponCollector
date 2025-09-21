using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PatternCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    private bool _isSelected = false;

    public int PatternId { get; private set; }

    public void Setup(Sprite sprite, int patternId)
    {
        _image.sprite = sprite;
        PatternId = patternId;
    }

    public void SetSelected(bool value)
    {
        _isSelected = value;
        _image.color = value ? Color.green : Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindFirstObjectByType<PatternManager>().SelectCard(this);
    }
}
