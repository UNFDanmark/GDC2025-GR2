using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnCooldown;
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCooldown)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-5, 5);
            spawnPosition.z += Random.Range(-5, 5);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            timer = 0;
        }
    }
}
