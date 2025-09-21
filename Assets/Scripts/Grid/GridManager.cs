using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private Button _generateCrystalButton;
    [SerializeField] private AudioManager _audioManager;

    [Header("Размер сетки")]
    [SerializeField] private int _gridSize = 6;

    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _generateCrystalButton.onClick.AddListener(GenerateGrid);
    }

    public void GenerateGrid()
    {
        _audioManager.PlayCrystalSpawn();

        foreach (Transform child in _gridParent)
            Destroy(child.gameObject);

        var gridLayout = _gridParent.GetComponent<GridLayoutGroup>();
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = _gridSize;

        int totalGridSize = _gridSize * _gridSize;

        for (int i = 0; i < totalGridSize; i++)
        {
            GameObject crystal = Instantiate(_crystalPrefab, _gridParent);
            crystal.GetComponent<Crystal>().SetRandomColor();
        }
    }

    public void TryRemoveCrystalAtScreenPoint(Vector2 screenPoint, Camera uiCamera)
    {
        RectTransform gridRect = _gridParent.GetComponent<RectTransform>();
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(gridRect, screenPoint, uiCamera, out Vector2 localPoint))
            return;

        Rect r = gridRect.rect;
        float xFromLeft = localPoint.x - r.xMin;
        float yFromTop = r.yMax - localPoint.y;

        float cellWidth = r.width / _gridSize;
        float cellHeight = r.height / _gridSize;

        int col = Mathf.FloorToInt(xFromLeft / cellWidth);
        int row = Mathf.FloorToInt(yFromTop / cellHeight);

        if (col < 0 || col >= _gridSize || row < 0 || row >= _gridSize)
            return;

        int childIndex = row * _gridSize + col;
        if (childIndex < 0 || childIndex >= _gridParent.childCount)
            return;

        Transform cellT = _gridParent.GetChild(childIndex);
        if (cellT == null) return;

        Crystal crystal = cellT.GetComponent<Crystal>();
        if (crystal != null)
        {
            crystal.RemoveCrystal();
        }
    }
}
