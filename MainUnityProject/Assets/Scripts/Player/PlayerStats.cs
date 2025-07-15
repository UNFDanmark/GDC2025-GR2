using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] string playerID;
    [SerializeField] Slider uiHealthBar;
    public int maxHealth;
    [SerializeField] float hitTime;
    [SerializeField] Material[] materials;
    [SerializeField] bool canRespawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] MeshRenderer[] body;
    [HideInInspector] public bool alive;
    public float speed;
    public InputAction move;
    public float rotationSpeed;
    public const int STATE_DEFAULT = 0, STATE_PARRY = 1, STATE_PARRY_HIT = 2, STATE_HIT = 3, STATE_DEAD = 4, RESPAWNING = 5;
    [HideInInspector] public ParryScript parry;
    public string ID => playerID;
    // PRIVATE
    int state;
    bool oofed;
    float remainingHitTime;
    PlayerRespawn playerRespawn;
    int health;
    static int initializedPlayers;
    // ---------------- METHODS ----------------
    public void SetState(int value) {
        if (value is STATE_HIT or STATE_PARRY_HIT) {
            oofed = true;
            remainingHitTime = hitTime;
        }
        foreach (var _mesh in body) {
            _mesh.material = materials[value];
        }
        state = value;
    }
    public int GetState() {
        return state;
    }
    public bool GetParry() {
        return state switch {
            STATE_PARRY => true,
            STATE_PARRY_HIT => true,
            _ => false
        };
    }
    public void DoDamage(int amount)
    {
        health -= amount;
        if (health <= 0) {
            health = 0;
            uiHealthBar.value = health;
            Die();
        }
        else
        {
            uiHealthBar.value = health;
        }
    }

    public void SetHealth(int amount) {
        health = amount;
        uiHealthBar.value = health;
    }
    void Awake() {
        playerRespawn = GetComponent<PlayerRespawn>();
        SetState(STATE_DEFAULT);
        parry = GetComponent<ParryScript>();
        move.Enable();
        alive = true;
    }
    void Start() {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        uiHealthBar.maxValue = maxHealth;
        health = maxHealth;
        uiHealthBar.value = health;
        cameraMovement.SetOffset();
    }
    void Update()
    {
        if (state is STATE_DEAD) return;
        remainingHitTime -= Time.deltaTime;
        if (oofed && remainingHitTime < 0) {
            oofed = false;
            switch (state) {
                case STATE_HIT:
                    SetState(STATE_DEFAULT);
                    break;
                case STATE_PARRY_HIT:
                    SetState(STATE_PARRY);
                    break;
            }
        }
    }
    void Die() {
        alive = false;
        if (canRespawn) {
            var _renders = GetComponentsInChildren<MeshRenderer>();
            foreach (var _render in _renders) {
                _render.enabled = false;
            }
            GetComponent<Rigidbody>().isKinematic = true;
            var _colliders = GetComponents<CapsuleCollider>();
            foreach (var _collider in _colliders) {
                _collider.enabled = false;
            }
            playerRespawn.StartRespawning();
            state = STATE_DEAD;
        }
        else {
            SceneManager.LoadScene("GameReplicaForTesting");
        }
    }
}
