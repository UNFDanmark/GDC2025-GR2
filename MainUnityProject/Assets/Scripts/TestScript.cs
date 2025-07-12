using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    bool hit;
    [SerializeField] float maxDistance;
    RaycastHit hitInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance);
        
    }

    void OnDrawGizmos()
    {
        if (hit && hitInfo.transform.CompareTag("Player"))
        {
            Gizmos.color = new Color32(25, 255, 0, 255);
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
    }
}
