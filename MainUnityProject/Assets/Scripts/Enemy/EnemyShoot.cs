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
		var _rb = _bullet.GetComponent<Rigidbody>();
		_rb.mass = enemySpawner.bulletKnockback;
		_rb.linearVelocity = transform.forward * enemySpawner.bulletSpeed;
		// ReSharper restore Unity.PerformanceCriticalCodeInvocation
	}
	void Awake() {
		enemyNavigation = GetComponentInParent<EnemyNavigation>();
	}
	void Update() {
		hit = Physics.Raycast(transform.position, transform.forward, out hitInfo,enemySpawner.targetRange);
		if (enemyNavigation.Stopped()) stareTimer += Time.deltaTime * enemySpawner.stoppedEnemyShootModifier;
		else if (hit && hitInfo.transform.CompareTag("Player")) stareTimer += Time.deltaTime;
		else stareTimer -= Time.deltaTime * enemySpawner.stareTimeDecreaseModifier;
		
		if (stareTimer >= enemySpawner.stareTimeBeforeShot) {
			// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
			Shoot();
			stareTimer = 0;
		}
	}
	void OnDrawGizmos()
	{
		if (hit && hitInfo.transform.CompareTag("Player")) Gizmos.color = Color.green;
		else Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position,transform.forward * enemySpawner.targetRange);
	}
}