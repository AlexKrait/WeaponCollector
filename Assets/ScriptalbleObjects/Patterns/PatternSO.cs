using UnityEngine;

[CreateAssetMenu(fileName = "NewPattern", menuName = "Patterns/Pattern")]
public class PatternSO : ScriptableObject
{
    [Header("Идентификация")]
    public string patternName;      // читаемое имя паттерна
    public int patternId;          // id рисунка (одинаковые id => одинаковые карточки)

    [Header("Спрайт карточки (правой панели)")]
    public Sprite cardSprite;

    [Header("Сетка 6x6: координаты ячеек, которые активны (x = column, y = row)")]
    public Vector2Int[] activeCells; // значения 0..5 по x и y
}
