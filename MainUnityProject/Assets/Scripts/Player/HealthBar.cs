using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] RawImage empty;
    [SerializeField] RawImage full;
    [SerializeField] Rect imgSize;
    [SerializeField] float imgSpacing;
    Slider slider;
    PlayerStats playerStats;
    float width;
    void Awake() {
        slider = GetComponent<Slider>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Start() {
        empty.uvRect = imgSize;
        full.uvRect = imgSize;
        width = imgSize.width + imgSpacing * playerStats.maxHealth;
        for (var i = 0; i < playerStats.maxHealth; i++) {
            var _pos = transform.position;
            _pos.x -= width / 2;
            _pos.x += i * imgSize.width + imgSpacing;
            var _img = Instantiate(empty.gameObject, _pos, Quaternion.identity);
            _img.GetComponent<RawImage>().uvRect = imgSize;
        }
    }

    void Update() {
    }
}
