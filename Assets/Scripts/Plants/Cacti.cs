using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacti : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    private Transform projectileSpawn;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private List<GameObject> ObjNear = new List<GameObject>();
    private GameObject currTarget;
    private bool isShooting;
    void Start()
    {
        projectileSpawn = transform.GetChild(0);
    }
    private void Update()
    {
        ObjNear.Clear();
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, 10f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit)
        {
            if (item.transform.gameObject.CompareTag("Enemy"))
            {
                ObjNear.Add(item.transform.gameObject);
            }
            else { currTarget = null;}
        }
        if (ObjNear.Count > 0)
        {
            float closestObjDist = 10f;
            foreach (GameObject item in ObjNear)
            {
                if (Vector3.Distance(this.transform.position, item.transform.position) < closestObjDist)
                {
                    currTarget = item.gameObject;
                    closestObjDist = Vector3.Distance(this.transform.position, item.transform.position);
                }
            }
        }
        
        if (currTarget != null)
        {
            Vector3 dir = currTarget.transform.position - transform.position;
            dir = dir.normalized;
            transform.forward = dir;
            if (!isShooting)
                StartCoroutine(shoot());
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
