using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    int maxHealth;
    public int health;
    EnemySpawner enemySpawner;
    public void Die() {
        Destroy(gameObject);
    }
    public void SetScript(EnemySpawner script) {
        enemySpawner = script;
        maxHealth = enemySpawner.enemyHealth;
        health = maxHealth;
    }
}
