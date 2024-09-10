using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlazingBloom : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private GameObject explosionEffect;
    private void Start()
    {
        StartCoroutine(attack());
    }
    IEnumerator attack()
    {
        while (true)
        {
            transform.localScale = transform.localScale * 2/3f;
            yield return new WaitForSeconds(2f);
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Enemy"))
                {
                    item.transform.gameObject.GetComponent<EnemyStats>().takeDamage(damage);
                }
            }
            transform.localScale = transform.localScale * 1.5f;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
