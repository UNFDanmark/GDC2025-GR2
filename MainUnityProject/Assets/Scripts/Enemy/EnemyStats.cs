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
    float oofTimer;
    Material originalEnemyMat;
    MeshRenderer meshRenderer;
    // ---------------- METHODS ----------------
    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalEnemyMat = meshRenderer.material;
    }
    void Update()
    {
        oofTimer += Time.deltaTime;
        if (oofTimer >= oofTime) meshRenderer.material = originalEnemyMat;
    }
    public void DoDamage(int amount) {
        health -= amount;
        if (health <= 0) Die();
        else
        {
            meshRenderer.material = oofMaterial;
            oofTimer = 0;
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
