using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	// ---------------- FIELDS ----------------
	[SerializeField] float disappearTime;
	[SerializeField] int baseDamage;
	[SerializeField] float bulletSpeed;
	[SerializeField] int ricochets;
	[SerializeField] float mass;
	// PRIVATE
	float bounceTime = 0.1f;
	float bounceTimer;
	bool bounceCooldown;
	float lifeTimer;
	GameObject bulletOrigin;
	Rigidbody rb;
	SphereCollider sphereCollider;
	PhysicsMaterial originalBounce;
	int playerDamage, enemyDamage;
	// ---------------- METHODS ----------------
	void Awake() {
		rb = GetComponent<Rigidbody>();
		sphereCollider = GetComponent<SphereCollider>();
		originalBounce = sphereCollider.material;
	}

	void Start() {
		rb.mass = mass;
		rb.linearVelocity = bulletOrigin.transform.forward * bulletSpeed;
	}

	public void SetOrigin(GameObject origin) {
		bulletOrigin = origin;
	}
	public void OverridePlayerDamage(int amount) {
		playerDamage = amount - baseDamage;
	}
	public void OverrideEnemyDamage(int amount) {
		enemyDamage = amount - baseDamage;
	}
	void Update() {
		lifeTimer += Time.deltaTime;
		if (bounceCooldown) bounceTimer += Time.deltaTime;
		if (lifeTimer >= disappearTime) Destroy(gameObject);
		if (bounceTimer >= bounceTime) {
			bounceCooldown = true;
			sphereCollider.material = originalBounce;
		}
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			var _player = other.gameObject.GetComponent<PlayerStats>();
			_player.DoDamage(playerDamage + baseDamage);
			Destroy(gameObject);
		}
		else if (other.gameObject.CompareTag("Enemy")) {
			var _enemy = other.gameObject.GetComponent<EnemyStats>();
			_enemy.DoDamage(enemyDamage + baseDamage);
			Destroy(gameObject);
		}

		if (ricochets > 0) {
			if (!bounceCooldown) {
				ricochets--;
				bounceCooldown = true;
				bounceTimer = 0;
				sphereCollider.material = null;
			}
		} 
		else Destroy(gameObject);
	}
}
