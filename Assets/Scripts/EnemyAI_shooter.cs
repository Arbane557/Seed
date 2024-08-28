using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyAI_shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private GameObject projectile;
    private Transform projectileSpawn;
    [SerializeField]
    private LayerMask layermask;
    private Vector3 dir;
    private bool isShooting;
    void Start()
    {
        projectileSpawn = transform.GetChild(1);
    }
    void Update()
    {
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 20f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
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

        if(targetObj != null)
        {
            dir = targetObj.transform.position - transform.position; 
            dir = dir.normalized;
            transform.forward = dir;
            if(!isShooting)
            StartCoroutine(shoot());
        }
        else
        {
            StopAllCoroutines();
            isShooting = false;
        }
    }

    IEnumerator shoot()
    {
        while (true)
        {
            isShooting = true;
            yield return new WaitForSeconds(1.9f);
            GameObject bullet = Instantiate(projectile.gameObject, projectileSpawn.position, this.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        }      
    }
}
    