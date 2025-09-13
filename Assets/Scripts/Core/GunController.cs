using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootPosition;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _strikeSpeed = 10f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(hor * _moveSpeed, rb.linearVelocity.y);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_shootPosition == null)
            {
                Debug.LogError("Shoot Position not assigned!");
                return;
            }

            GameObject bullet = Instantiate(_bulletPrefab, _shootPosition.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = Vector2.up * _strikeSpeed;
            }
        }
    }
}
