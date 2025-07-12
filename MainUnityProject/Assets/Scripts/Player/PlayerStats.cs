using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField] int maxHealth;
    public int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        print("DEAD");
    }
}
