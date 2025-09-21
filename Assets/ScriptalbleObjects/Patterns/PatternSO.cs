using UnityEngine;

[CreateAssetMenu(fileName = "NewPattern", menuName = "Patterns/Pattern")]
public class PatternSO : ScriptableObject
{
    [Header("�������������")]
    public string patternName;      // �������� ��� ��������
    public int patternId;          // id ������� (���������� id => ���������� ��������)

    [Header("������ �������� (������ ������)")]
    public Sprite cardSprite;

    [Header("����� 6x6: ���������� �����, ������� ������� (x = column, y = row)")]
    public Vector2Int[] activeCells; // �������� 0..5 �� x � y
}
