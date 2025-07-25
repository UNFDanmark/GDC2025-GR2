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
	[SerializeField] float bounceTime;
	// PRIVATE
	float remainingBounceTime;
	bool bounceCooldown;
	float lifeTimer;
	GameObject bulletOrigin;
	Rigidbody rb;
	Collider bulletCollider;
	PhysicsMaterial originalBounce;
	int playerDamage, enemyDamage, environmentDamage;
	// ---------------- METHODS ----------------
	void Awake() {
		rb = GetComponent<Rigidbody>();
		bulletCollider = GetComponent<Collider>();
		originalBounce = bulletCollider.material;
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
	public void OverrideEnvironmentDamage(int amount) {
		environmentDamage = amount - baseDamage;
	}
	public void OverrideEnemyDamage(int amount) {
		enemyDamage = amount - baseDamage;
	}
	void Update() {
		lifeTimer += Time.deltaTime;
		if (bounceCooldown) remainingBounceTime -= Time.deltaTime;
		if (lifeTimer >= disappearTime) Destroy(gameObject);
		if (remainingBounceTime < 0) {
			bounceCooldown = true;
			bulletCollider.material = originalBounce;
		}
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			var _player = other.gameObject.GetComponent<PlayerStats>();
			_player.SetState(PlayerStats.STATE_HIT);
			_player.DoDamage(playerDamage + baseDamage);
			Destroy(gameObject);
		}
		else if (other.gameObject.CompareTag("Enemy")) {
			var _enemy = other.gameObject.GetComponent<EnemyStats>();
			_enemy.DoDamage(enemyDamage + baseDamage);
			Destroy(gameObject);
		}
		else if (other.gameObject.CompareTag("Environment")) {
			var _environment = other.gameObject.GetComponentInParent<EnvironmentStats>();
			_environment.DoDamage(environmentDamage + baseDamage);
			Destroy(gameObject);
		}
		if (ricochets > 0) {
			if (!bounceCooldown) {
				ricochets--;
				bounceCooldown = true;
				remainingBounceTime = bounceTime;
				bulletCollider.material = null;
			}
		} 
		else Destroy(gameObject);
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") && other.GetType() == typeof(CapsuleCollider)) {
			var _player = other.gameObject.GetComponentInParent<PlayerStats>();
			if (_player.GetParry()) {
				MusicManager.PlaySound(MusicManager.Instance.parry2,true);
				var _newDirection = other.gameObject.transform.forward;
				_newDirection *= rb.linearVelocity.magnitude + _player.parry.addedBulletSpeed;
				var _newPosition = other.gameObject.transform.position + other.gameObject.transform.forward;
				_newPosition.y = transform.position.y;
				transform.position = _newPosition;
				GetComponentInChildren<Transform>().Rotate(0,0,180);
				rb.linearVelocity = _newDirection;
				_player.SetState(PlayerStats.STATE_PARRY_HIT);
			}
		}
	}
}
