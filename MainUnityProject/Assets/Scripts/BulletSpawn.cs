using System;
using System.ComponentModel;
using UnityEngine;

public class BulletSpawn : MonoBehaviour {
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    public void Shoot() {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed;
    }
}
