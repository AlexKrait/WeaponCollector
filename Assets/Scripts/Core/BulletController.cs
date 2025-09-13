using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3f; // ����� ����� ����

    void Start()
    {
        Destroy(gameObject, _lifeTime); // ������� ���� ����� 3 �������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            Destroy(collision.gameObject); // ���������� ����
            Destroy(gameObject);           // ���������� ����
        }
    }
}
