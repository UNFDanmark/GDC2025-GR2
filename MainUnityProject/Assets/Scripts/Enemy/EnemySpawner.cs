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
    [SerializeField] bool firstSpawnInstant;
    [SerializeField] bool randomSpawnHeight;
    [Header("Enemy Navigation")] 
    public float walkSpeed;
    public float turnSpeed;
    public float stoppingDistance;
    public float stoppedEnemyRotationSpeed;
    [Header("Enemy Attack")]
    public GameObject bulletPrefab;
    public float stoppedEnemyShootModifier;
    public float stareTimeBeforeShot;
    public float targetRange;
    public float bulletSpeed;
    public float stareTimeDecreaseModifier;
    public int damageToPlayers;
    public int damageToOtherEnemies;
    [Header("Enemy Stats")]
    public int enemyHealth;
    // PRIVATE
    float spawnTimer;
    Transform playerTransform;
    int remainingSpawns;
    // ---------------- METHODS ----------------
    void Awake() {
        GetComponent<MeshRenderer>().enabled = false;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    void Start() {
        if (firstSpawnInstant) spawnTimer = spawnCooldown;
        remainingSpawns = totalSpawnAmount;
    }
    void Update()
    {
        if (!playerTransform) return;
        spawnTimer += Time.deltaTime;
        // ReSharper disable Unity.PerformanceCriticalCodeInvocation
        if (remainingSpawns > 0 && spawnTimer >= spawnCooldown)
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
            spawnTimer = 0;
        }
        // ReSharper restore Unity.PerformanceCriticalCodeInvocation
    }
}
