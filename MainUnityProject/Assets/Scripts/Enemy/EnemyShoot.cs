using System;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
	// ---------------- FIELDS ----------------
	
	// PRIVATE
	EnemySpawner enemySpawner;
	EnemyNavigation enemyNavigation;
	RaycastHit hitInfo;
	float stareTimer;
	bool hit;
	// ---------------- METHODS ----------------
	public void SetScript(EnemySpawner script) {
		enemySpawner = script;
	}
	void Shoot() {
		// ReSharper disable Unity.PerformanceCriticalCodeInvocation
		var _bullet = Instantiate(enemySpawner.bulletPrefab, transform.position, Quaternion.identity);
		var _bs = _bullet.GetComponent<BulletBehaviour>();
		_bs.SetOrigin(gameObject);
		_bs.OverrideEnemyDamage(enemySpawner.damageToOtherEnemies);
		_bs.OverridePlayerDamage(enemySpawner.damageToPlayers);
		// ReSharper restore Unity.PerformanceCriticalCodeInvocation
	}
	void Awake() {
		enemyNavigation = GetComponentInParent<EnemyNavigation>();
	}
	void Update() {
		hit = Physics.Raycast(transform.position, transform.forward, out hitInfo,enemySpawner.targetRange);
		hit = hit && hitInfo.transform == enemySpawner.targetPlayer.transform;
		if (enemyNavigation.Stopped()) stareTimer += Time.deltaTime * enemySpawner.stoppedEnemyShootModifier;
		else if (hit) stareTimer += Time.deltaTime;
		else stareTimer -= Time.deltaTime * enemySpawner.stareTimeDecreaseModifier;
		if (stareTimer >= enemySpawner.stareTimeBeforeShot) {
			// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
			Shoot();
			stareTimer = 0;
		}
	}
	void OnDrawGizmos() {
		Gizmos.color = hit ? Color.green : Color.red;
		Gizmos.DrawRay(transform.position,transform.forward * enemySpawner.targetRange);
	}
}