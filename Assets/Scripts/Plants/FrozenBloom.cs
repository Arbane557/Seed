using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrozenBloom : MonoBehaviour
{
   
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private List<GameObject> slowedEnemy = new List<GameObject>();
    private bool isInitiated = false;
    private void Start()
    {
    }
    private void Update()
    {
        if (!isInitiated) { StartCoroutine(attack()); isInitiated = true; }
    }
    IEnumerator attack()
    {
        while (true)
        {
            transform.localScale = transform.localScale * 2 / 3f;        
            slowedEnemy.Clear();
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Enemy")) slowedEnemy.Add(item.transform.gameObject);      
            }
            foreach (var item in slowedEnemy) { item.transform.gameObject.GetComponent<NavMeshAgent>().speed -= 3; }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            transform.localScale = transform.localScale * 1.5f;
            yield return new WaitForSeconds(5f);
            foreach (var e in slowedEnemy) { e.transform.gameObject.GetComponent<NavMeshAgent>().speed += 3; }
        }
    }
}
