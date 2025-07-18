using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    // SOUNDS
    public static MusicManager Instance;
    void Awake() {
        Instance = this;
    }
    public AudioSource explosion, explosionReverb, hurt, hurt1, hurt2, hurt3, kunaiThrow, parry2, parryMiss, shoot, walk, obstacleHit;
    [SerializeField] AudioSource[] transitions;
    [SerializeField] AudioSource[] sections;
    [SerializeField] float timerThreshold;
    float timer;
    int currentTrack;
    bool inTransition;
    public static void PlaySound(AudioSource sound, bool restart) {
        if (restart || !sound.isPlaying) sound.Play();
    }
    public static void StopSound(AudioSource sound) {
        if (sound.isPlaying) sound.Stop();
    }
    public void StopTrack() {
        sections[currentTrack].Stop();
    }
    public void StopTrack(int section) {
        sections[section].Stop();
    }
    public void StartTransition(int section, float headStart) {
        inTransition = true;
        currentTrack = section;
        transitions[section].Play();
        timer = transitions[section].clip.length-headStart;
    }
    public void StartTransition(int section) {
        StartTransition(section,timerThreshold);
    }

    public void StartTrack(int section) {
        currentTrack = section;
        sections[section].Play();
    }
    void Update() {
        timer -= Time.deltaTime;
        if (timer < 0 && inTransition) {
            sections[currentTrack].Play();
            inTransition = false;
        }
    }
}
