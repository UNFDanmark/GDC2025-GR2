using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float respawnTime;
    [SerializeField] float respawnTimeMin;
    [SerializeField] float respawnTimeMax;
    [SerializeField] Transform respawnLocation;
    // PRIVATE
    float respawnRemainingTime;
    bool respawning;
    PlayerStats playerStats;
    // ---------------- METHODS ----------------
    void Awake() {
        playerStats = GetComponent<PlayerStats>();
    }
    void Update() {
        respawnRemainingTime -= Time.deltaTime;
        if (respawning && respawnRemainingTime < 0) {
            // ReSharper disable Unity.PerformanceCriticalCodeInvocation
            transform.position = respawnLocation.position;
            transform.rotation = respawnLocation.rotation;
            playerStats.SetHealth(playerStats.maxHealth);
            var _renders = GetComponentsInChildren<MeshRenderer>();
            foreach (var _render in _renders) {
                _render.enabled = true;
            }
            GetComponent<Rigidbody>().isKinematic = false;
            var _colliders = GetComponents<CapsuleCollider>();
            foreach (var _collider in _colliders) {
                _collider.enabled = true;
            }
            playerStats.SetState(PlayerStats.STATE_DEFAULT);
            respawning = false;
            // ReSharper restore Unity.PerformanceCriticalCodeInvocation
        }
    }
    public void StartRespawning() {
        respawnRemainingTime = respawnTime;
        respawning = true;
    }
}
