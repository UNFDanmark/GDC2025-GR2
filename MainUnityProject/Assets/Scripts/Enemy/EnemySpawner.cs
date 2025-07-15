using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [Header("Spawning")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnCooldown;
    [SerializeField] int totalSpawnAmount;
    [SerializeField] float firstSpawnTime;
    [SerializeField] bool randomSpawnHeight;
    [Header("Enemy Navigation")] 
    public float walkSpeed;
    public float turnSpeed;
    public float stoppingDistance;
    public float stoppedEnemyRotationSpeed;
    [Header("Enemy Attack")]
    public GameObject bulletPrefab;
    public GameObject targetPlayer;
    public float stoppedEnemyShootModifier;
    public float stareTimeBeforeShot;
    public float targetRange;
    public float stareTimeDecreaseModifier;
    public int damageToPlayers;
    public int damageToOtherEnemies;
    [Header("Enemy Stats")]
    public int enemyHealth;
    // PRIVATE
    float remainingSpawnTime;
    Transform playerTransform;
    int remainingSpawns;
    // ---------------- METHODS ----------------
    void Start() {
        remainingSpawnTime = firstSpawnTime;
        remainingSpawns = totalSpawnAmount;
    }
    void Update()
    {
        remainingSpawnTime -= Time.deltaTime;
        // ReSharper disable Unity.PerformanceCriticalCodeInvocation
        if (remainingSpawns > 0 && remainingSpawnTime < 0)
        {
            var _spawnPosition = transform.position;
            _spawnPosition.x += Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            if (randomSpawnHeight) _spawnPosition.y += Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2);
            _spawnPosition.z += Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);
            var _enemy = Instantiate(enemyPrefab, _spawnPosition, Quaternion.identity); 
            _enemy.gameObject.GetComponentInChildren<EnemyShoot>().SetScript(this);
            _enemy.gameObject.GetComponent<EnemyNavigation>().SetScript(this);
            _enemy.gameObject.GetComponent<EnemyStats>().SetScript(this);
            remainingSpawns--;
            remainingSpawnTime = spawnCooldown;
        }
        // ReSharper restore Unity.PerformanceCriticalCodeInvocation
    }
}
