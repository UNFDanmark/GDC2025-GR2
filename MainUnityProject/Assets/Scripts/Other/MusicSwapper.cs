using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSwapper : MonoBehaviour {
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] AudioSource whiteNoise;
    bool started;
    int currentAudio;
    float timer;
    void OnTriggerEnter(Collider other) {
        if (other.isTrigger) return;
        if (other.transform.CompareTag("Player")) {
            var _player = other.GetComponent<PlayerStats>();
            if (_player.ID == "Player1") {
                whiteNoise.Stop();
                started = true;
            }
        }
    }

    void Update() {
        timer -= Time.deltaTime;
        if (started) {
            if (!audioSources[currentAudio].isPlaying) {
                audioSources[currentAudio].Play();
                timer = audioSources[currentAudio].clip.length;
            }
            if (timer < 0) {
                currentAudio++;
            }
        }
    }
}
