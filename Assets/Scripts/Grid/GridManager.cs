using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] GameObject _crystalPrefab;
    [SerializeField] Transform _gridParent;
    [SerializeField] Button _generateCrystalButton;

    [Header("Размер сетки")]
    [SerializeField] int _gridSize = 6;

    private void Start()
    {
        _generateCrystalButton.onClick.AddListener(GenerateGrid);
    }

    public void GenerateGrid()
    {
        foreach (Transform child in _gridParent)
        {
            Destroy(child.gameObject);
        }

        var layot = _gridParent.GetComponent<GridLayoutGroup>();
        layot.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layot.constraintCount = _gridSize;

        int total = _gridSize * _gridSize;

        for (int i = 0; i < total; i++)
        {
            GameObject crystal = Instantiate(_crystalPrefab, _gridParent);
            crystal.GetComponent<Crystal>().SetRandomColor();
        }
    }
}
