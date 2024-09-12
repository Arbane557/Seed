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

    private bool isAttack;
    [SerializeField]
    private bool hasAttacked;
    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask layermask;
    private Vector3 targetLoc; [SerializeField]
    private List<GameObject> ObjNear = new List<GameObject>();
    void Start()
    {
        
        agent = gameObject.GetComponent<NavMeshAgent>();
        randomSpawnLoc();
    }

    void FixedUpdate()
    {
        ObjNear.Clear();
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 7f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit)
        {
            if (item.transform.gameObject.CompareTag("Player"))
            {
                ObjNear.Add(item.transform.gameObject);
            }
            else { targetObj = GameObject.FindGameObjectWithTag("Main Tree"); }
        }
        if (ObjNear.Count > 0)
        {
            float closestObjDist = 10f;
            foreach (GameObject item in ObjNear)
            {
                if (Vector3.Distance(this.transform.position, item.transform.position) < closestObjDist)
                {
                    targetObj = item.gameObject;
                    closestObjDist = Vector3.Distance(this.transform.position, item.transform.position);
                }
            }
        }

        if (targetObj != null)
        {
            transform.LookAt(targetObj.transform.position);
            Vector3 targetDir = (targetObj.transform.position - transform.position).normalized;
            if (Vector3.Distance(transform.position, targetObj.transform.position) > 3f)
            {
                StopAllCoroutines();
                hasAttacked = false;
                isAttack = false;
                agent.SetDestination(targetObj.transform.position);
            }
            else
            {
                if (!hasAttacked)
                {
                    StartCoroutine(attack());
                    hasAttacked = true;
                }
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            transform.LookAt(targetLoc);
            agent.SetDestination(targetLoc);
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
            Debug.Log("true");
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 3f, transform.forward, 1, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (!isAttack)
                {
                    if (item.transform.gameObject.CompareTag("Player") || item.transform.gameObject.CompareTag("Main Tree"))
                    {
                        isAttack = true;
                        item.transform.gameObject.GetComponent<PlayerStats>().damage(damage);
                    }
                }
            }
            yield return new WaitForSeconds(2f);
            isAttack = false;
            Debug.Log("false");
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
