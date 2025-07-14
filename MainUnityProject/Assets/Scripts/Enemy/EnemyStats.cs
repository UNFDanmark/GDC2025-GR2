using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [SerializeField] float oofTime;
    [SerializeField] Material oofMaterial;
    // PRIVATE
    EnemySpawner enemySpawner;
    int maxHealth;
    int health;
    float remainingOofTime;
    Material originalEnemyMat;
    MeshRenderer meshRenderer;
    // ---------------- METHODS ----------------
    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalEnemyMat = meshRenderer.material;
    }
    void Update()
    {
        remainingOofTime -= Time.deltaTime;
        if (remainingOofTime < 0) meshRenderer.material = originalEnemyMat;
    }
    public void DoDamage(int amount) {
        health -= amount;
        if (health <= 0) Die();
        else
        {
            meshRenderer.material = oofMaterial;
            remainingOofTime = oofTime;
        }
    }
    void Die() {
        Destroy(gameObject);
    }
    public void SetScript(EnemySpawner script) {
        enemySpawner = script;
        maxHealth = enemySpawner.enemyHealth;
        health = maxHealth;
    }
}
