using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float respawnTime;
    [SerializeField] float respawnTimeMax;
    [SerializeField] float respawnTimeMin;
    [SerializeField] float timeFadeModifier;
    [SerializeField] float deathTimeAdd;
    [HideInInspector] public bool respawning;
    [SerializeField] CameraMovement cameraMovement;

    [SerializeField] Vector3 respawnOffset;
    // PRIVATE
    float respawnRemainingTime;

    PlayerStats playerStats;
    // ---------------- METHODS ----------------
    void Awake() {
        playerStats = GetComponent<PlayerStats>();
    }
    void Update() {
        respawnRemainingTime -= Time.deltaTime;
        if (!respawning) respawnTime -= Time.deltaTime * timeFadeModifier;
        if (respawnTime < respawnTimeMin) respawnTime = respawnTimeMin;
        if (respawning && respawnRemainingTime < 0) {
            // ReSharper disable Unity.PerformanceCriticalCodeInvocation
            playerStats.SetHealth(playerStats.maxHealth);
            transform.position = cameraMovement.GetP2Spawn() + respawnOffset;
            transform.rotation = Quaternion.identity;
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
        respawnTime += deathTimeAdd;
        if (respawnTime > respawnTimeMax) respawnTime = respawnTimeMax;
        respawnRemainingTime = respawnTime;
        respawning = true;
    }
}
