using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform player1;
    [SerializeField] GameObject player2;
    [SerializeField] float player1Weight;
    [SerializeField] float player2Weight;
    [SerializeField] Transform averageObject;
    [SerializeField] float transitionModifer;
    [SerializeField] float cinematicFocusWeight;
    [SerializeField] float cinematicTime;
    [SerializeField] float cinematicZoom;
    float remainingCinematicTime;
    float cinematicsCustomWeight;
    float player2CustomWeight;
    bool cinematics;
    Vector3 cinematicFocus;
    Vector3 cameraOffset;
    Vector3 currentOffset;
    Vector3 average;
    PlayerStats playerRespawn;
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
            if (currentOffset.y > cinematicZoom) currentOffset.y -= Time.deltaTime * transitionModifer;
        }
        else {
            if (cinematicsCustomWeight > 0) cinematicsCustomWeight -= Time.deltaTime * transitionModifer;
            if (currentOffset.y < cameraOffset.y) currentOffset.y += Time.deltaTime * transitionModifer;
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
        average = (player1.position * player1Weight + player2.transform.position * player2CustomWeight + cinematicFocus * cinematicsCustomWeight) / (player1Weight+player2CustomWeight+cinematicsCustomWeight);
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
