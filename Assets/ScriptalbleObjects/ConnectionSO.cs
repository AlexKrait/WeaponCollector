using UnityEngine;

[CreateAssetMenu(fileName = "ConnectionSO", menuName = "Patterns/Connection")]
public class ConnectionSO : ScriptableObject
{
    public int patternId;                // совпадает с PatternCard.PatternId
    public Sprite previewSprite;         // картинка для 2-сек показа
    public Vector2Int[] activeCells;     // координаты активных ячеек (0..5, 0..5)
}
