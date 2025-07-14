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
    float remainingParryCooldown;
    float remainingParryDuration;
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
	    parryKey.Enable();
    }
    void Update() {
	    if (playerStats.GetState() is PlayerStats.STATE_DEAD) return;
	    remainingParryCooldown -= Time.deltaTime;
	    remainingParryDuration -= Time.deltaTime;
	    if (playerStats.GetParry() && remainingParryDuration < 0) {
		    playerStats.SetState(PlayerStats.STATE_DEFAULT);
		    parryTrigger.enabled = false;
	    }
	    if (parryKey.WasPressedThisFrame() && remainingParryCooldown < 0) {
		    remainingParryCooldown = parryCooldown;
		    remainingParryDuration = parryDuration;
		    playerStats.SetState(PlayerStats.STATE_PARRY);
		    parryTrigger.enabled = true;
	    }
    }
}
