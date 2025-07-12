using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] float itemSpawnCooldown;

    [SerializeField] GameObject itemPrefab;
    [SerializeField] int spawnRadius;

    float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > itemSpawnCooldown)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-spawnRadius, spawnRadius+1);
            spawnPosition.z += Random.Range(-spawnRadius, spawnRadius+1);
            spawnPosition.y += itemPrefab.transform.localScale.x / 2;
            Instantiate(itemPrefab, spawnPosition, itemPrefab.transform.rotation);
            timePassed = 0;
        }
    }
}
