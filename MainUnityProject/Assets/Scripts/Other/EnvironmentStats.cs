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
    // PRIVATE
    const int DESTROY_WIN_AMOUNT = 4;
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
        MusicManager.PlaySound(MusicManager.Instance.obstacleHit,true);
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
            case "Wall1":
                MusicManager.Instance.StopTrack();
                MusicManager.PlaySound(MusicManager.Instance.explosionReverb,true);
                break;
            case "Wall2":
                MusicManager.Instance.StopTrack();
                MusicManager.PlaySound(MusicManager.Instance.explosion,true);
                MusicManager.Instance.StartTransition(2,1);
                break;
             case "ShieldGen":
                 MusicManager.PlaySound(MusicManager.Instance.explosion,true);
                 break;
        }
        destroyedAmount++;
        if (destroyedAmount >= DESTROY_WIN_AMOUNT) {
            Reset();
            SceneManager.LoadScene("Main Menu");
        }
        Destroy(gameObject);
    }
}
