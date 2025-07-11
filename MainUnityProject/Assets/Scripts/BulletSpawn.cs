using UnityEngine;

public class BulletSpawn : MonoBehaviour {
    [SerializeField] GameObject bulletPrefab;

    public void Shoot() {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
