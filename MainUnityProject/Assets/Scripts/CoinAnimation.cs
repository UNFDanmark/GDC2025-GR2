using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }
}
