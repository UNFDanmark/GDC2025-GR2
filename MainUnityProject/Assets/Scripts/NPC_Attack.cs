using System;
using UnityEngine;

public class NPC_Attack : MonoBehaviour {
    BulletSpawn bulletSpawner;
    [SerializeField] float stareTime;
    float sightTimer;
    float rayHeight = 1f;
    RaycastHit hitInfo;
    bool hit;
    Vector3 pos;
    [SerializeField] float maxDistance;

    void Awake() {
        bulletSpawner = GetComponentInChildren<BulletSpawn>();
    }

    void Update() {
        pos = new Vector3(transform.position.x, transform.position.y + rayHeight, transform.position.z);
        
        hit = Physics.Raycast(pos, transform.forward, out hitInfo,maxDistance);
        if (hit && hitInfo.transform.CompareTag("Player")) sightTimer += Time.deltaTime;
        if (sightTimer >= stareTime) {
            sightTimer = 0;
            bulletSpawner.Shoot();
        }
    }

    void OnDrawGizmos()
    {
        if (hit && hitInfo.transform.CompareTag("Player"))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(pos,transform.forward * maxDistance);
    }
}
