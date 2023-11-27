using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform SpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;//Calculate angle of trajectory and convert to Rad
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotate arrow to angle of trajectory
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.position = SpawnPoint.position;
    }
}
