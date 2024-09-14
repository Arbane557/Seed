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
    private bool isInitiated = false;
    private void Start()
    {
    }
    private void Update()
    {
        if (!isInitiated) {StartCoroutine(attack()); isInitiated = true;}    
    }
    IEnumerator attack()
    {
        while (true)
        {
            transform.localScale = transform.localScale * 2/3f;
            yield return new WaitForSeconds(4f);
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("hit");
                    item.transform.gameObject.GetComponent<EnemyStats>().takeDamage(damage);
                }
            }
            transform.localScale = transform.localScale * 1.5f;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
