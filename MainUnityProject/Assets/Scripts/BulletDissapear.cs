using UnityEngine;

public class BulletDissapear : MonoBehaviour {
    [SerializeField] float dissapearTime;
    float timer;
    void Update() {
        timer += Time.deltaTime;
        if (timer >= dissapearTime) Destroy(gameObject);
    }
}
