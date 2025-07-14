using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{  
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] int damageToOtherPlayer;
    [SerializeField] int damageToEnemy;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingCooldown;
    [SerializeField] InputAction shootKey;
    //PRIVATE
    float remainingShootingCooldown;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update() {
        remainingShootingCooldown -= Time.deltaTime;
        if (shootKey.WasPressedThisFrame() && remainingShootingCooldown < 0) {
            remainingShootingCooldown = shootingCooldown;
            var _bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            var _bs = _bullet.GetComponent<BulletBehaviour>();
            _bs.SetOrigin(gameObject);
            _bs.OverrideEnemyDamage(damageToEnemy);
            _bs.OverridePlayerDamage(damageToOtherPlayer);
        }
    }
}
