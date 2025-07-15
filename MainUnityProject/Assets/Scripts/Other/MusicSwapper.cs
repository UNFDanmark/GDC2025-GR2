using System;
using UnityEngine;

public class MusicSwapper : MonoBehaviour {
    [SerializeField] AudioSource audioSource;
    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player")) {
            var _player = other.GetComponent<PlayerStats>();
            if (_player.ID == "Player1") {
                audioSource.Play();
            }
        }
    }
}
