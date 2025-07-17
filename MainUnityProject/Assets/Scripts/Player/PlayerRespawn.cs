using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float respawnTime;
    [SerializeField] float respawnTimeMax;
    [SerializeField] float respawnTimeMin;
    [SerializeField] float timeFadeModifier;
    [SerializeField] float deathTimeAdd;
    [SerializeField] Slider respawnTimer;
    [HideInInspector] public bool respawning;
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject respawnSmoke;
    // PRIVATE
    float respawnRemainingTime;
    PlayerStats playerStats;
    // ---------------- METHODS ----------------
    void Awake() {
        playerStats = GetComponent<PlayerStats>();
    }
    void Update() {
        respawnRemainingTime -= Time.deltaTime;
        if (respawnTimer) respawnTimer.value = respawnRemainingTime;
        print(respawnRemainingTime);
        if (!respawning) respawnTime -= Time.deltaTime * timeFadeModifier;
        if (respawnTime < respawnTimeMin) respawnTime = respawnTimeMin;
        if (respawning && respawnRemainingTime < 0) {
            // ReSharper disable Unity.PerformanceCriticalCodeInvocation
            playerStats.alive = true;
            if (respawnTimer) respawnTimer.gameObject.SetActive(false);
            playerStats.SetHealth(playerStats.maxHealth);
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            Instantiate(respawnSmoke, respawnPoint.position, respawnPoint.rotation);
            var _renders = GetComponentsInChildren<MeshRenderer>();
            foreach (var _render in _renders) {
                _render.enabled = true;
            }
            GetComponent<Rigidbody>().isKinematic = false;
            var _colliders = GetComponentsInChildren<Collider>();
            foreach (var _collider in _colliders) {
                _collider.enabled = true;
            }

            GetComponent<Collider>().enabled = true;
            GetComponent<ParryScript>().enabled = true;
            GetComponent<PlayerShoot>().enabled = true;
            GetComponent<PlayerMovement>().enabled = true;
            GetComponentInChildren<Canvas>().enabled = true;
            playerStats.model.SetActive(true);
            playerStats.SetState(PlayerStats.STATE_DEFAULT);
            respawning = false;
            // ReSharper restore Unity.PerformanceCriticalCodeInvocation
        }
    }
    public void StartRespawning() {
        if (respawnTimer) respawnTimer.gameObject.SetActive(true);
        respawnTime += deathTimeAdd;
        if (respawnTime > respawnTimeMax) respawnTime = respawnTimeMax;
        respawnRemainingTime = respawnTime;
        respawning = true;
        if (respawnTimer) respawnTimer.maxValue = respawnTime;
    }
}
