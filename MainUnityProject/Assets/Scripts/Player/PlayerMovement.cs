using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {
    // ---------------- FIELDS ----------------
    
    // PRIVATE
    Rigidbody rb;
    PlayerStats playerStats;
    // ---------------- METHODS ----------------
    void Awake() {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Update() {
        if (playerStats.GetState() is PlayerStats.STATE_DEAD) return;
        var _movementVector = playerStats.move.ReadValue<Vector2>();
        var _newVelocity = rb.linearVelocity;
        _newVelocity.x = _movementVector.x * playerStats.speed;
        _newVelocity.z = _movementVector.y * playerStats.speed;
        rb.linearVelocity = _newVelocity;
        if (rb.linearVelocity.sqrMagnitude != 0) {
            var _rotationVector = _newVelocity;
            _rotationVector.y = transform.rotation.y;
            _rotationVector.Normalize();
            //var _desiredRotation = Quaternion.LookRotation(_rotationVector);
            //transform.rotation = _desiredRotation;
            var _temp = transform.rotation;
            transform.LookAt(transform.position + rb.linearVelocity);
            var _final = transform.rotation;
            transform.rotation = _temp;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _final,playerStats.rotationSpeed);
        }
    }
}
