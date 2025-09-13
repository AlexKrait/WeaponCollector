using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _boxPrefab;
    [SerializeField] float _spawnInterval = 2f;
    [SerializeField] float _spawnHeight = 6f;
    [SerializeField] float _spawnXRange = 7f;

    void Start()
    {
        StartCoroutine(SpawnBoxes());
    }

    IEnumerator SpawnBoxes()
    {
        while (true)
        {
            float randomX = Random.Range(-_spawnXRange, _spawnXRange);
            Vector3 spawnPos = new Vector3(randomX, _spawnHeight, 0f);
            GameObject box = Instantiate(_boxPrefab, spawnPos, Quaternion.identity);

            // ”станавливаем гравитацию дл€ каждого €щика
            Rigidbody2D rb = box.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.05f;
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
