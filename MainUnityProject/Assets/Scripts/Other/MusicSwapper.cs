using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSwapper : MonoBehaviour {
    [SerializeField] int section;
    [SerializeField] bool transition;
    [SerializeField] float headStart;
    bool started;
    float timer;
    int currentAudio;

    void OnTriggerEnter(Collider other) {
        if (other.isTrigger || started) return;
        if (other.transform.CompareTag("Player")) {
            var _player = other.GetComponent<PlayerStats>();
            if (_player.ID == "Player1") {
                started = true;
                MusicManager.Instance.StopTrack();
                if (transition && headStart != 0) MusicManager.Instance.StartTransition(section, headStart);
                else if (transition) MusicManager.Instance.StartTransition(section);
                else MusicManager.Instance.StartTrack(section);
            }
        }
    }
}
