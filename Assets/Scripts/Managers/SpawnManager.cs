using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float _timer = 0f;
    [SerializeField] float _spawnInterval = 2f;
    [SerializeField] float _spawnHeight = 6f;
    [SerializeField] GameObject _boxPrefab;
    [SerializeField] Rigidbody2D _rb;
    


    void Start()
    {
        
    }


    void Update()
    {
        _timer += Time.deltaTime;
        _rb.gravityScale = 0.05f;
        
        if (_timer > _spawnInterval)
        {
            _timer = 0f;
            float randomPositionX = Random.Range(-7f, 7f);
            Vector3 spawnPosition = new Vector3(randomPositionX, _spawnHeight, 0f);
            GameObject box = Instantiate(_boxPrefab, spawnPosition, Quaternion.identity); 
        }
    }
}
