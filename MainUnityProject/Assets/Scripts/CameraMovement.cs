using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] float player1Weight;
    [SerializeField] float player2Weight;
    [SerializeField] Transform averageObject;
    Vector3 cameraOffset;
    Vector3 average;
    void Update() {
        average = (player1.position * player1Weight + player2.position * player2Weight) / (player1Weight+player2Weight);
        transform.position = average + cameraOffset;
        averageObject.position = average;
    }
    public void SetOffset() {
        average = (player1.position * player1Weight + player2.position * player2Weight) / (player1Weight+player2Weight);
        cameraOffset = transform.position - average;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(average,2);
    }
}
