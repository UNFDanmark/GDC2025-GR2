using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerID;
    [SerializeField] Slider uiHealthBar;
    [SerializeField] int maxHealth;
    [SerializeField] float oofTime;
    [SerializeField] Material oofMaterial;
    MeshRenderer meshRenderer;
    Material originalPlayerMat;
    float oofTimer;
    int health;
    public string ID => playerID;
    static int initializedPlayers;
    
    public void DoDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
        else
        {
            meshRenderer.material = oofMaterial;
            oofTimer = 0;
            uiHealthBar.value = health;
        }
    }
    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalPlayerMat = meshRenderer.material;
    }
    void Start()
    {
        uiHealthBar.maxValue = maxHealth;
        health = maxHealth;
        uiHealthBar.value = health;
    }
    void Update()
    {
        oofTimer += Time.deltaTime;
        if (oofTimer >= oofTime) meshRenderer.material = originalPlayerMat;
    }
    void Die()
    {
        SceneManager.LoadScene("Enemy AI");
    }
}
