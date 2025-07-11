using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] InputAction move;
    Rigidbody rigidbody;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        move.Enable();
    }

    void Update() {
        Vector2 movementVector = move.ReadValue<Vector2>();
        Vector3 newVelocity = rigidbody.linearVelocity;
        newVelocity.x = movementVector.x * speed;
        newVelocity.z = movementVector.y * speed;
        rigidbody.linearVelocity = newVelocity;
    }
}
