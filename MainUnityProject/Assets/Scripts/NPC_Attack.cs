using System;
using UnityEngine;

public class NPC_Attack : MonoBehaviour {
    BulletSpawn bulletSpawner;
    [SerializeField] float stareTime;
    float sightTimer;
    float rayHeight = 1f;

    void Awake() {
        bulletSpawner = GetComponentInChildren<BulletSpawn>();
    }

    void Update() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + rayHeight, transform.position.z);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(pos, transform.forward, out hitInfo);
        if (hit && hitInfo.transform.CompareTag("Player")) sightTimer += Time.deltaTime;
        if (sightTimer >= stareTime) {
            sightTimer = 0;
            bulletSpawner.Shoot();
        }
    }
}
