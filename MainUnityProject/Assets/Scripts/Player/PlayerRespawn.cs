using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float respawnTime;
    [SerializeField] float respawnTimeMin;
    [SerializeField] float respawnTimeMax;
    [SerializeField] Vector3 respawnLocation;
    // PRIVATE
    float respawnTimer;
    bool respawning;
    // ---------------- METHODS ----------------
    void Update() {
        respawnTimer += Time.deltaTime;
        if (respawning && respawnTimer >= respawnTime) {
            // RESPAWN LOGIC HERE
            
        }
    }

    public void StartRespawning() {
        respawnTimer = 0;
        respawning = true;
    }
}
