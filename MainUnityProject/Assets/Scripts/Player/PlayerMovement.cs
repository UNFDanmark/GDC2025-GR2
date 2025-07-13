using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float speed;
    [SerializeField] InputAction move;
    [SerializeField] float rotationSpeed;
    [SerializeField] InputAction rotationAction;
    // PRIVATE
    Rigidbody rb;
    // ---------------- METHODS ----------------
    void Awake() {
        rb = GetComponent<Rigidbody>();
        move.Enable();
        rotationAction.Enable();
    }
    void Update() {
        var _movementVector = move.ReadValue<Vector2>();
        var _newVelocity = rb.linearVelocity;
        _newVelocity.x = _movementVector.x * speed;
        _newVelocity.z = _movementVector.y * speed;
        rb.linearVelocity = _newVelocity;
        transform.Rotate(0, rotationAction.ReadValue<float>() * rotationSpeed, 0);
    }
}
