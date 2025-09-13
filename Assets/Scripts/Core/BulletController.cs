using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3f; // время жизни пули

    void Start()
    {
        Destroy(gameObject, _lifeTime); // удаляем пулю через 3 секунды
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            Destroy(collision.gameObject); // уничтожаем ящик
            Destroy(gameObject);           // уничтожаем пулю
        }
    }
}
