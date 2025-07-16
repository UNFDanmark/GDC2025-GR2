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
    [SerializeField] MusicSwapper musicToStop;
    [SerializeField] MusicSwapper musicToStart;
    // PRIVATE
    const int DESTROY_WIN_AMOUNT = 5;
    static int destroyedAmount;
    int health;
    float remainingOofTime;
    Material originalEnvironmentMat;
    MeshRenderer meshRenderer;

    // ---------------- METHODS ----------------
    void Awake() {
        destroyedAmount = 0;
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
        switch (obstacleName) {
            case "Wall":
                musicToStop.Stop();
                if (musicToStart) musicToStart.Play();
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
