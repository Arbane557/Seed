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
    [SerializeField]
    private List<GameObject> ObjNear = new List<GameObject>();
    void Start()
    {
        startSize = transform.localScale;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        ObjNear.Clear();
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 12f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
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
            if (!inAttackRange)
            {
                if (Vector3.Distance(transform.position, targetObj.transform.position) > 3f)
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
            if (item.transform.gameObject.CompareTag("Player") || item.transform.gameObject.CompareTag("Main Tree"))
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
