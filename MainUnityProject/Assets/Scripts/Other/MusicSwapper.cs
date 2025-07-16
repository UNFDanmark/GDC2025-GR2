using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSwapper : MonoBehaviour {
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] AudioSource whiteNoise;
    
    bool started, stopped;
    int currentAudio;
    float timer;
    void OnTriggerEnter(Collider other) {
        if (other.isTrigger || started) return;
        if (other.transform.CompareTag("Player")) {
            var _player = other.GetComponent<PlayerStats>();
            if (_player.ID == "Player1") {
                Play();
            }
        }
    }
    void Update() {
        timer -= Time.deltaTime;
        if (started && !stopped) {
            if (!audioSources[currentAudio].isPlaying) {
                audioSources[currentAudio].Play();
                timer = audioSources[currentAudio].clip.length-0.01f;
            }
            if (timer < 0 && !audioSources[currentAudio].loop) {
                currentAudio++;
            }
        }
    }
    public void Play() {
        if (whiteNoise) whiteNoise.Stop();
        started = true;
    }
    public void Stop() {
        stopped = true;
        audioSources[currentAudio].Stop();
        whiteNoise.Play();
    }
}
