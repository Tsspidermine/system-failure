using UnityEngine;
using UnityEngine.VFX;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public Transform player;
    public Transform target;
    public GameObject explosionEffect;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameObject impactGO = Instantiate(explosionEffect, this.transform.position, Quaternion.LookRotation(player.transform.position));
        if(target != null) 
        {
            impactGO.gameObject.transform.localScale = target.gameObject.transform.localScale;
        }
        Destroy(impactGO, 2.5f);
    }
}
