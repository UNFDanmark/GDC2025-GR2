using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParryScript : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [SerializeField] float parryCooldown;
	public int addedBulletDamage;
	public float addedBulletSpeed;
	[SerializeField] InputAction parryKey;
	[SerializeField] float parryDuration;
    // PRIVATE
    float parryCooldownTimer;
    float parryDurationTimer;
    PlayerStats playerStats;
    Vector3 reflectDirection;
    CapsuleCollider parryTrigger;
    // ---------------- METHODS ----------------
    void Awake() {
	    playerStats = GetComponent<PlayerStats>();
	    foreach (var _collider in GetComponents<CapsuleCollider>()) {
		    if (_collider.isTrigger) parryTrigger = _collider;
	    }
    }
    void Start() {
	    parryCooldownTimer = parryCooldown;
	    parryKey.Enable();
    }
    void Update() {
	    if (playerStats.GetState() is PlayerStats.STATE_DEAD) return;
	    parryCooldownTimer += Time.deltaTime;
	    parryDurationTimer += Time.deltaTime;
	    if (playerStats.GetParry() && parryDurationTimer >= parryDuration) {
		    playerStats.SetState(PlayerStats.STATE_DEFAULT);
		    parryTrigger.enabled = false;
	    }
	    if (parryKey.WasPressedThisFrame() && parryCooldownTimer >= parryCooldown) {
		    parryCooldownTimer = 0;
		    parryDurationTimer = 0;
		    playerStats.SetState(PlayerStats.STATE_PARRY);
		    parryTrigger.enabled = true;
	    }
    }
}
