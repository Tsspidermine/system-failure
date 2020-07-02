using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float damage = 10f;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;

    private bool isAttacking = false;

    Transform target;
    public Transform playerPos;
    public Transform firePos;
    NavMeshAgent agent;
    public GameObject projectile;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.speed = 5f;

            if (distance <= agent.stoppingDistance)
            {
                Attack(); //Attack
                FaceTarget(); //Look at Me
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Attack()
    {
        if(isAttacking == false){
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire() {
        isAttacking = true;
        GameObject bulletGO = Instantiate(projectile, firePos.position, Quaternion.LookRotation(this.transform.position));
        bulletGO.GetComponent<EnemyBullet>().damage = damage;
        bulletGO.transform.LookAt(playerPos.position);
        Destroy(bulletGO, 2f);
        yield return new WaitForSeconds(fireRate);
        isAttacking = false;
    }
}
