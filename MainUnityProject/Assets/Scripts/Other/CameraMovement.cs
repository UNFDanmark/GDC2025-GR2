using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform player1;
    [SerializeField] GameObject player2;
    [SerializeField] float player1Weight;
    [SerializeField] float player2Weight;
    [SerializeField] float p1Weight2;
    [SerializeField] float p2Weight2;
    [SerializeField] Transform averageObject;
    [SerializeField] float transitionModifer;
    [SerializeField] float zoomModifier;
    [SerializeField] float cinematicFocusWeight;
    [SerializeField] float cinematicTime;
    [SerializeField] Vector3 cinematicZoom;
    public bool cinematicTrigger;
    float remainingCinematicTime;
    float cinematicsCustomWeight;
    float player2CustomWeight;
    bool cinematics;
    Vector3 cinematicFocus;
    Vector3 cameraOffset;
    Vector3 currentOffset;
    Vector3 average;
    PlayerStats playerRespawn;

    public void ChangeWeight() {
        player1Weight = p1Weight2;
        player2Weight = p2Weight2;
    }
    void Awake() {
        playerRespawn = player2.GetComponent<PlayerStats>();
        player2CustomWeight = player2Weight;
    }
    void Update() {
        remainingCinematicTime -= Time.deltaTime;
        if (remainingCinematicTime < 0) cinematics = false;
        if (cinematics) {
            if (cinematicsCustomWeight < cinematicFocusWeight) {
                cinematicsCustomWeight += Time.deltaTime * transitionModifer;
            }
            if (currentOffset.x > cinematicZoom.x) currentOffset.x -= Time.deltaTime * zoomModifier;
            if (currentOffset.y > cinematicZoom.y) currentOffset.y -= Time.deltaTime * zoomModifier;
            if (currentOffset.z > cinematicZoom.z) currentOffset.z -= Time.deltaTime * zoomModifier;
        }
        else if (!cinematicTrigger) {
            if (cinematicsCustomWeight > 0) cinematicsCustomWeight -= Time.deltaTime * transitionModifer;
            if (currentOffset.x < cameraOffset.x) currentOffset.x += Time.deltaTime * zoomModifier;
            if (currentOffset.y < cameraOffset.y) currentOffset.y += Time.deltaTime * zoomModifier;
            if (currentOffset.z < cameraOffset.z) currentOffset.z += Time.deltaTime * zoomModifier;
            if (cinematicsCustomWeight <= 0 && currentOffset.x >= cameraOffset.x && currentOffset.y >= cameraOffset.y &&
                currentOffset.z >= cameraOffset.z) cinematicTrigger = true;
        }
        if (playerRespawn.alive) {
            if (player2CustomWeight < player2Weight) player2CustomWeight += Time.deltaTime * transitionModifer;
        }
        else {
            if (player2CustomWeight > 0) player2CustomWeight -= Time.deltaTime * transitionModifer;
        }
        average = (player1.position * player1Weight + player2.transform.position * player2CustomWeight + cinematicFocus * cinematicsCustomWeight) / (player1Weight+player2CustomWeight+cinematicsCustomWeight);
        transform.position = average + currentOffset;
        averageObject.position = average;
    }
    public void SetOffset() {
        average = (player1.position * player1Weight + player2.transform.position * player2CustomWeight) / (player1Weight+player2CustomWeight);
        cameraOffset = transform.position - average;
        currentOffset = cameraOffset;
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(average,2);
    }
    public void StartCinematic(Vector3 position) {
        remainingCinematicTime = cinematicTime;
        cinematics = true;
        cinematicFocus = position;
    }
}
