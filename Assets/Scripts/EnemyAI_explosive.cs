using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_explosive : MonoBehaviour
{
    private float x, z;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float movementForce;
    [SerializeField]
    private float damage;
    private bool inAttackRange;

    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask layermask;
  
    private Vector3 targetLoc;
    private Vector3 startSize;
    void Start()
    {
        startSize = transform.localScale;
        agent = gameObject.GetComponent<NavMeshAgent>();
        randomSpawnLoc();
    }

    void FixedUpdate()
    {
        targetLoc = new Vector3(x, 1, z);

        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 15f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit)
        {
            if (item.transform.gameObject.CompareTag("Player"))
            {
                targetObj = item.transform.gameObject;
            }
           
        }

        if (targetObj != null)
        {
            transform.LookAt(targetObj.transform.position);
            Vector3 targetDir = (targetObj.transform.position - transform.position).normalized;
            if (!inAttackRange)
            {
                if (Vector3.Distance(transform.position, targetObj.transform.position) > 2f)
                {
                    inAttackRange = false;
                    agent.SetDestination(targetObj.transform.position);
                }
                else
                {
                    transform.localScale = startSize;
                    Pulse();
                    inAttackRange = true;
                    agent.SetDestination(transform.position);
                    StartCoroutine(attack());
                }
            }
        }
        else
        {
            agent.SetDestination(targetLoc);
            transform.LookAt(targetLoc);
            if (Vector3.Distance(transform.position, targetLoc) < 1f)
            {
                randomSpawnLoc();
            }
        }


    }

    IEnumerator attack()
    {

        yield return new WaitForSeconds(2f);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 4f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit)
        {
            if (item.transform.gameObject.CompareTag("Player"))
            {
                item.transform.gameObject.GetComponent<PlayerStats>().damage(damage);
            }       
        }
        transform.localScale = transform.localScale * 1.7f;
        Debug.Log("BOOMMMM!!!");
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void randomSpawnLoc()
    {
        x = Random.Range(transform.position.x - 5, transform.position.x + 5);
        z = Random.Range(transform.position.z - 5, transform.position.z + 5);
        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            randomSpawnLoc();
        }
    }
    private void Pulse()
    {
        transform.localScale = transform.localScale * 1.2f;
    }
}
