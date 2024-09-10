using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManEater : MonoBehaviour
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
        if (!isInitiated) { StartCoroutine(attack()); isInitiated = true; }
    }
    IEnumerator attack()
    {
        while (true)
        {
            transform.localScale = transform.localScale * 2 / 3f;
            yield return new WaitForSeconds(7f);
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Enemy"))
                {
                    Destroy(item.transform.gameObject);
                }
            }
            transform.localScale = transform.localScale * 1.5f;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
