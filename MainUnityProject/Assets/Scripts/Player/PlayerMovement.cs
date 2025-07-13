using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {
    // ---------------- FIELDS ----------------
    [SerializeField] float speed;
    [SerializeField] InputAction move;
    [SerializeField] float rotationSpeed;
    // PRIVATE
    Rigidbody rb;
    // ---------------- METHODS ----------------
    void Awake() {
        rb = GetComponent<Rigidbody>();
        move.Enable();
    }
    void Update() {
        var _movementVector = move.ReadValue<Vector2>();
        var _newVelocity = rb.linearVelocity;
        _newVelocity.x = _movementVector.x * speed;
        _newVelocity.z = _movementVector.y * speed;
        rb.linearVelocity = _newVelocity;
        if (rb.linearVelocity.sqrMagnitude != 0) {
            Vector3 _rotationVector = _newVelocity;
            _rotationVector.y = transform.rotation.y;
            _rotationVector.Normalize();
            //var _desiredRotation = Quaternion.LookRotation(_rotationVector);
            //transform.rotation = _desiredRotation;
            var _temp = transform.rotation;
            transform.LookAt(transform.position + rb.linearVelocity);
            var _final = transform.rotation;
            transform.rotation = _temp;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _final,rotationSpeed);
        }
    }
}
