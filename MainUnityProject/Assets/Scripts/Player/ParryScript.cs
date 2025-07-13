using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParryScript : MonoBehaviour
{
    // ---------------- FIELDS ----------------
    [SerializeField] float parryCooldown;
	[SerializeField] int newBulletDamage;
	[SerializeField] float newBulletSpeed;
	[SerializeField] InputAction parryKey;
	[SerializeField] float parryDuration;
	[SerializeField] Material parryMaterial;
	public bool parryActive;
    // PRIVATE
    float parryCooldownTimer;
    float parryDurationTimer;
    PlayerMovement playerMovement;
    Vector3 reflectDirection;
    MeshRenderer meshRenderer;
    Material originalMaterial;
    // ---------------- METHODS ----------------
    void Awake() {
	    playerMovement = GetComponent<PlayerMovement>();
	    meshRenderer = GetComponentInChildren<MeshRenderer>();
	    originalMaterial = meshRenderer.material;
    }
    void Start() {
	    parryCooldownTimer = parryCooldown;
	    parryKey.Enable();
    }
    void Update() {
	    parryCooldownTimer += Time.deltaTime;
	    parryDurationTimer += Time.deltaTime;
	    print(parryActive);
	    if (parryActive && parryDurationTimer >= parryDuration) {
		    parryActive = false;
		    meshRenderer.material = originalMaterial;
	    }
	    if (parryKey.WasPressedThisFrame() && parryCooldownTimer >= parryCooldown) {
		    Parry();
	    }
    }
    void Parry() {
	    parryCooldownTimer = 0;
	    parryDurationTimer = 0;
	    parryActive = true;
	    meshRenderer.material = parryMaterial;
    }
}
