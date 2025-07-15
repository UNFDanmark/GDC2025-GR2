using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentStats : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [SerializeField] float oofTime;
    [SerializeField] Material oofMaterial;
    [SerializeField] int maxHealth;
    [SerializeField] string obstacleName;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] AudioSource audioSource;
    // PRIVATE
    static readonly int DESTROY_WIN_AMOUNT;
    static int destroyedAmount;
    int health;
    float remainingOofTime;
    Material originalEnvironmentMat;
    MeshRenderer meshRenderer;
    static EnvironmentStats() {
        DESTROY_WIN_AMOUNT = 3;
    }

    // ---------------- METHODS ----------------
    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalEnvironmentMat = meshRenderer.material;
        health = maxHealth;
    }
    void Update()
    {
        remainingOofTime -= Time.deltaTime;
        if (remainingOofTime < 0) meshRenderer.material = originalEnvironmentMat;
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

    public static void Reset() {
        destroyedAmount = 0;
    }

    void Die()
    {
        if (cameraMovement) {
            var _cinematic = transform.position;
            _cinematic.z -= 2;
            cameraMovement.StartCinematic(_cinematic);
        }
        switch (name) {
            case "Wall1":
                audioSource.Stop();
                break;
        }
        destroyedAmount++;
        if (destroyedAmount >= DESTROY_WIN_AMOUNT) {
            Reset();
            SceneManager.LoadScene("GameReplicaForTesting");
        }
        Destroy(gameObject);
    }
}
