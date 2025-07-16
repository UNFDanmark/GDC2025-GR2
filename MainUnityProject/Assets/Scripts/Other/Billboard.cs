using System;
using UnityEngine;

public class Billboard : MonoBehaviour {
    [SerializeField] Transform cameraTransform;

    void LateUpdate() {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
