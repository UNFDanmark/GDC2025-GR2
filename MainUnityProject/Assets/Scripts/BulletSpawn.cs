using System;
using UnityEngine;

public class BulletSpawn : MonoBehaviour {
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    Rigidbody bulletRB;

    void Awake()
    {
        bulletRB = bulletPrefab.GetComponent<Rigidbody>();
    }

    public void Shoot() {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
    }
}
