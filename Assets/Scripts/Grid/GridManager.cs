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

    private void Start()
    {
        _generateCrystalButton.onClick.AddListener(GenerateGrid);
    }

    public void GenerateGrid()
    {
        _audioManager.PlayCrystalSpawn();
        foreach (Transform child in _gridParent)
        {
            Destroy(child.gameObject);
        }

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
}
