using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage;

    public Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward towards the player's last known direction;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
    }
}
