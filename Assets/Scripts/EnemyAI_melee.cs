using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI_melee : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float movementForce;
    private float x, z;

    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask layermask;
    private Vector3 targetLoc;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        randomSpawnLoc();
    }

    void FixedUpdate()
    {
        targetLoc = new Vector3(x, 1, z);
        
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 10f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit)
        {
            if (item.transform.gameObject.CompareTag("Player"))
            {
                targetObj = item.transform.gameObject;
            }
            else
            {
                targetObj = null;
            }
        }

        if (targetObj != null)
        {
            transform.LookAt(targetObj.transform.position);
            Vector3 targetDir = (targetObj.transform.position - transform.position).normalized;
            if (Vector3.Distance(transform.position, targetObj.transform.position) > 3f)
            {
                StopAllCoroutines();
                agent.SetDestination(targetObj.transform.position);
            }
            else
            {
                StartCoroutine(attack());
                agent.SetDestination(transform.position);
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
        while (true)
        {
            yield return new WaitForSeconds(2f);
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 3f, transform.forward, 1, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Player"))
                {
                    item.transform.gameObject.GetComponent<PlayerStats>().damage(damage);
                }
            }
        }
    }

    private void randomSpawnLoc()
    {
        x = Random.Range(transform.position.x - 5, transform.position.x + 5);
        z = Random.Range(transform.position.z - 5, transform.position.z + 5);
        if(agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            randomSpawnLoc();
        }
    }
}
