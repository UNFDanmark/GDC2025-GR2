using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParryScript : MonoBehaviour
{
	static readonly int SPIN = Animator.StringToHash("ParryActive");

	// ---------------- FIELDS ----------------
    [SerializeField] float parryCooldown;
	public float addedBulletSpeed;
	[SerializeField] InputAction parryKey;
	[SerializeField] float parryDuration;
	[Header("AimAssist")] 
	[SerializeField] bool enableAimAssist;
	[SerializeField] float radius;
	[SerializeField] float rotationSpeed;
	[SerializeField] GameObject targetPlayer;
	[SerializeField] bool instantParry;
	public readonly List<Transform> EnemiesInTrigger = new();
	// PRIVATE
	float remainingParryCooldown;
	float remainingParryDuration;
    PlayerStats playerStats, targetPlayerStats;
    Vector3 reflectDirection;
    CapsuleCollider parryTrigger;
    SphereCollider parryTargetTrigger;
    Transform aimTarget;
    Quaternion desiredRotation;
    Animator animator;
    // ---------------- METHODS ----------------
    void Awake() {
	    animator = GetComponentInChildren<Animator>();
	    playerStats = GetComponent<PlayerStats>();
	    targetPlayerStats = targetPlayer.GetComponent<PlayerStats>();
	    var _capsules = GetComponentsInChildren<CapsuleCollider>();
	    foreach (var _capsule in _capsules) {
		    if (_capsule.isTrigger) parryTrigger = _capsule;
	    }
	    parryTargetTrigger = GetComponentInChildren<SphereCollider>();
	    parryTargetTrigger.radius = radius;
    }
    void Start() {
	    parryKey.Enable();
    }
    void Update() {
	    if (!targetPlayerStats.alive) {
		    EnemiesInTrigger.Remove(targetPlayer.transform);
	    }
	    if (playerStats.GetState() is PlayerStats.STATE_DEAD) return;
	    remainingParryCooldown -= Time.deltaTime;
	    remainingParryDuration -= Time.deltaTime;
	    if (playerStats.GetParry() && remainingParryDuration < 0) {
		    playerStats.SetState(PlayerStats.STATE_DEFAULT);
		    parryTrigger.enabled = false;
		    animator.SetBool(SPIN, false);
	    }
	    if (parryKey.WasPressedThisFrame() && remainingParryCooldown < 0) {
		    MusicManager.PlaySound(MusicManager.Instance.kunaiThrow,true);
		    animator.SetBool(SPIN, true);
		    remainingParryCooldown = parryCooldown;
		    remainingParryDuration = parryDuration;
		    playerStats.SetState(PlayerStats.STATE_PARRY);
		    parryTrigger.enabled = true;
		    if (enableAimAssist) {
			    var _targetFound = false;
			    if (EnemiesInTrigger.Contains(targetPlayer.transform)) {
				    aimTarget = targetPlayer.transform;
				    _targetFound = true;
			    }
			    else {
				    foreach (var _enemy in EnemiesInTrigger) {
					    if (_enemy.CompareTag("Enemy")) {
						    // Make enemy target if null or lower dist
						    if (!aimTarget || Vector3.Distance(transform.position, aimTarget.position) >
						        Vector3.Distance(transform.position, _enemy.position)) aimTarget = _enemy;
					    }
					    _targetFound = true;
				    }
			    }
			    desiredRotation = _targetFound ? Quaternion.LookRotation(aimTarget.position - transform.position) : Quaternion.LookRotation(transform.position + transform.forward);
			    if (instantParry) transform.rotation = desiredRotation;
		    }
	    }
	    if (playerStats.GetParry() && !instantParry) transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed);
    }
    void OnTriggerEnter(Collider other) {
	    if (other.transform.CompareTag("Enemy")) {
		    EnemiesInTrigger.Add(other.transform);
	    }
	    if (other.transform.CompareTag("Player")) {
		    EnemiesInTrigger.Add(other.transform);
	    }
    }
    void OnTriggerExit(Collider other) {
	    if (other.transform.CompareTag("Enemy")) {
		    EnemiesInTrigger.Remove(other.transform);
	    }
	    if (other.transform.CompareTag("Player")) {
		    EnemiesInTrigger.Remove(other.transform);
	    }
    }

    void OnDrawGizmos() {
	    if (enableAimAssist) {
		    Gizmos.color = Color.yellow;
		    Gizmos.DrawWireSphere(transform.position,radius);
	    }
    }
}
