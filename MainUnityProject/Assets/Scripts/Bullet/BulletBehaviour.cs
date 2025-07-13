using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	// ---------------- FIELDS ----------------
	[SerializeField] float dissapearTime;
	[SerializeField] int baseDamage;
	// PRIVATE
	float timer;
	GameObject bulletOrigin;
	int playerDamage, enemyDamage;
	// ---------------- METHODS ----------------
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
		timer += Time.deltaTime;
		if (timer >= dissapearTime) Destroy(gameObject);
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			var _stats = other.gameObject.GetComponent<PlayerStats>();
			print(playerDamage+baseDamage);
			_stats.DoDamage(playerDamage + baseDamage);
			if (_stats.GetHealth() <= 0) _stats.Die();
		}
		else if (other.gameObject.CompareTag("Enemy")) {
			var _stats = other.gameObject.GetComponent<EnemyStats>();
			_stats.health -= enemyDamage + baseDamage;
			if (_stats.health <= 0) _stats.Die();
		}
		Destroy(gameObject);
	}
}
