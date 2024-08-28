using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private LayerMask layermask;

    void Start()
    {

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
            transform.LookAt(new Vector3(targetObj.transform.position.x,transform.position.y,targetObj.transform.position.z));
            StartCoroutine(shoot());
        }
        else
        {
            StopAllCoroutines();
            transform.LookAt(new Vector3(0,1,0));
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(3f);
        GameObject bullet = Instantiate(projectile.gameObject, transform.position, this.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
    }
}
