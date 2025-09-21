using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PatternGrid : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI refs")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private RectTransform _cellsParent;
    [SerializeField] private Color _inactiveColor = new Color(0.15f, 0.15f, 0.15f, 0.6f);
    [SerializeField] private Color _activeColor = new Color(1f, 0.9f, 0.2f, 1f);

    private Image[,] _cells = new Image[6, 6];
    private RectTransform _rect;
    private ConnectionSO _connection;
    private Canvas _rootCanvas;
    private Camera _uiCamera;
    private Vector2 _dragOffset;

    public void Initialize(ConnectionSO connection)
    {
        _connection = connection;
        _rect = GetComponent<RectTransform>();
        _rootCanvas = GetComponentInParent<Canvas>();
        _uiCamera = (_rootCanvas != null && _rootCanvas.renderMode != RenderMode.ScreenSpaceOverlay) ? _rootCanvas.worldCamera : null;

        BuildGrid();
        HighlightPattern();
    }

    private void BuildGrid()
    {
        foreach (Transform t in _cellsParent) Destroy(t.gameObject);

        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                GameObject cellGO = Instantiate(_cellPrefab, _cellsParent);
                Image img = cellGO.GetComponent<Image>();
                img.color = _inactiveColor;
                _cells[x, y] = img;
            }
        }
    }

    private void HighlightPattern()
    {
        if (_connection == null) return;
        foreach (var pos in _connection.activeCells)
        {
            if (pos.x >= 0 && pos.x < 6 && pos.y >= 0 && pos.y < 6)
                _cells[pos.x, pos.y].color = _activeColor;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_rect.parent, eventData.position, _uiCamera, out Vector2 localPoint);
        _dragOffset = _rect.anchoredPosition - localPoint;

        var cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_rect.parent, eventData.position, _uiCamera, out Vector2 localPoint);
        _rect.anchoredPosition = localPoint + _dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.blocksRaycasts = true;

        if (_connection == null) return;

        for (int i = 0; i < _connection.activeCells.Length; i++)
        {
            Vector2Int p = _connection.activeCells[i];
            int index = p.y * 6 + p.x;
            if (index < 0 || index >= _cellsParent.childCount) continue;

            RectTransform cellRect = _cellsParent.GetChild(index) as RectTransform;
            if (cellRect == null) continue;

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_uiCamera, cellRect.position);
            GridManager.Instance.TryRemoveCrystalAtScreenPoint(screenPoint, _uiCamera);
        }

        Destroy(gameObject);
    }
}
