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
    float spawnTimer;
    Transform playerTransform;
    MeshRenderer meshRenderer;
    int remainingSpawns;
    // ---------------- METHODS ----------------
    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

    }
    void Start() {
        if (firstSpawnInstant) spawnTimer = spawnCooldown;
        remainingSpawns = totalSpawnAmount;
    }
    void Update()
    {
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
