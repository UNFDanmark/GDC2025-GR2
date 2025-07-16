using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
	static readonly int SHOOT = Animator.StringToHash("Shoot");
	// ---------------- FIELDS ----------------
	
	// PRIVATE
	EnemySpawner enemySpawner;
	EnemyNavigation enemyNavigation;
	RaycastHit hitInfo;
	float remainingStareTimer;
	bool hit;
	PlayerStats playerStats;
	Animator animator;
	// ---------------- METHODS ----------------
	[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
	public void SetScript(EnemySpawner script) {
		enemySpawner = script;
		playerStats = enemySpawner.targetPlayer.GetComponent<PlayerStats>();
	}
	void Shoot() {
		// ReSharper disable Unity.PerformanceCriticalCodeInvocation
		MusicManager.PlaySound(MusicManager.Instance.shoot,true);
		print("Shoot");
		print(!animator);
		animator.SetTrigger(SHOOT);
		var _bullet = Instantiate(enemySpawner.bulletPrefab, transform.position, Quaternion.identity);
		var _bs = _bullet.GetComponent<BulletBehaviour>();
		_bs.SetOrigin(gameObject);
		_bs.OverrideEnemyDamage(enemySpawner.damageToOtherEnemies);
		_bs.OverridePlayerDamage(enemySpawner.damageToPlayers);
		_bs.OverrideEnvironmentDamage(enemySpawner.damageToEnvironment);
		// ReSharper restore Unity.PerformanceCriticalCodeInvocation
	}
	void Awake() {
		enemyNavigation = GetComponentInParent<EnemyNavigation>();
		animator = GetComponentInChildren<Animator>();
	}
	void Update() {
		if (playerStats.GetState() is PlayerStats.STATE_DEAD) return;
		hit = Physics.Raycast(transform.position, transform.forward, out hitInfo,enemySpawner.targetRange);
		hit = hit && hitInfo.transform == enemySpawner.targetPlayer.transform;
		if (enemyNavigation.Stopped()) remainingStareTimer -= Time.deltaTime * enemySpawner.stoppedEnemyShootModifier;
		else if (hit) remainingStareTimer -= Time.deltaTime;
		else remainingStareTimer += Time.deltaTime * enemySpawner.stareTimeDecreaseModifier;
		if (remainingStareTimer < 0) {
			// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
			Shoot();
			remainingStareTimer = enemySpawner.stareTimeBeforeShot;
		}
	}
	void OnDrawGizmos() {
		if(!enemySpawner) return;
		Gizmos.color = hit ? Color.green : Color.red;
		Gizmos.DrawRay(transform.position,transform.forward * enemySpawner.targetRange);
	}
}