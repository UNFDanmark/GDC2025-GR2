using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Slider uiHealthBar;
    [SerializeField] int maxHealth;
    [SerializeField] float oofTime;
    [SerializeField] Material oofMaterial;
    MeshRenderer meshRenderer;
    Material originalPlayerMat;
    float oofTimer;
    int health;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalPlayerMat = meshRenderer.material;
    }

    public void SetHealth(int amount)
    {
        health = amount;
    }

    public void DoDamage(int amount)
    {
        health -= amount;
        if (amount <= 0) Die();
        else
        {
            meshRenderer.material = oofMaterial;
            oofTimer = 0;
            uiHealthBar.value = health;
        }
    }

    public int GetHealth()
    {
        return health;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiHealthBar.maxValue = maxHealth;
        health = maxHealth;
        uiHealthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        oofTimer += Time.deltaTime;
        if (oofTimer >= oofTime) meshRenderer.material = originalPlayerMat;
    }

    public void Die()
    {
        SceneManager.LoadScene("Enemy AI");
    }
}
