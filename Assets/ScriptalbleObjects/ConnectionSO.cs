using UnityEngine;

[CreateAssetMenu(fileName = "ConnectionSO", menuName = "Patterns/Connection")]
public class ConnectionSO : ScriptableObject
{
    public int patternId;                // ��������� � PatternCard.PatternId
    public Sprite previewSprite;         // �������� ��� 2-��� ������
    public Vector2Int[] activeCells;     // ���������� �������� ����� (0..5, 0..5)
}
