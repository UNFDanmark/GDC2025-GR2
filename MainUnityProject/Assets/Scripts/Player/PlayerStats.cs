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
    [SerializeField] Image[] fullHearts;
    public int maxHealth;
    [SerializeField] float hitTime;
    [SerializeField] Material[] materials;
    [SerializeField] bool canRespawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SkinnedMeshRenderer body;
    public GameObject model;
    [HideInInspector] public bool alive;
    public float speed;
    public InputAction move;
    public float rotationSpeed;
    public const int STATE_DEFAULT = 0, STATE_PARRY = 1, STATE_PARRY_HIT = 2, STATE_HIT = 3, STATE_DEAD = 4;
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
        body.material = materials[value];
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
        MusicManager.PlaySound(MusicManager.Instance.hurt,true);
        health -= amount;
        if (health <= 0) {
            health = 0;
            UpdateHealthUI(health);
            Die();
        }
        else
        {
            UpdateHealthUI(health);
        }
    }

    void UpdateHealthUI(int amount) {
        uiHealthBar.value = amount;
        for (var _i = 0; _i < maxHealth; _i++) {
            fullHearts[_i].gameObject.SetActive(false);
        }
        for (var _i = 0; _i < amount; _i++) {
            fullHearts[_i].gameObject.SetActive(true);
        }
    }

    public void SetHealth(int amount) {
        health = amount;
        UpdateHealthUI(health);
    }
    void Awake() {
        materials[0] = body.material;
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
        UpdateHealthUI(health);
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
            var _colliders = GetComponentsInChildren<Collider>();
            foreach (var _collider in _colliders) {
                _collider.enabled = false;
            }
            GetComponent<Collider>().enabled = false;
            GetComponent<ParryScript>().enabled = false;
            GetComponent<PlayerShoot>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
            model.SetActive(false);
            playerRespawn.StartRespawning();
            state = STATE_DEAD;
        }
        else {
            SceneManager.LoadScene(3);
        }
    }
}
