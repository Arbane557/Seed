using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrozenBloom : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private List<GameObject> slowedEnemy = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(attack());
    }
    IEnumerator attack()
    {
        while (true)
        {
            transform.localScale = transform.localScale * 2 / 3f;
            foreach (var e in slowedEnemy) {e.transform.GetChild(0).gameObject.GetComponent<NavMeshAgent>().speed += 2; }
            yield return new WaitForSeconds(5f);
            slowedEnemy.Clear();
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Enemy"))
                {
                    slowedEnemy.Add(item.transform.gameObject);
                    item.transform.GetChild(0).gameObject.GetComponent<NavMeshAgent>().speed -= 2;
                }
            }
            transform.localScale = transform.localScale * 1.5f;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
