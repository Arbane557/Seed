using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantSunflower : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private LayerMask layermask;
    [SerializeField]
    private List<GameObject> buffedPlants = new List<GameObject>();
    private bool isInitiated = false;
   
    private void Update()
    {
        if (!isInitiated) { StartCoroutine(attack()); isInitiated = true; }
    }
    IEnumerator attack()
    {
        while (true)
        {
            buffedPlants.Clear();
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Plant"))
                {
                    buffedPlants.Add(item.transform.gameObject);
                }
            }
            transform.localScale = transform.localScale * 1.5f;
            foreach (var item in buffedPlants)
            {
                item.GetComponent<PlantStatus>().IsBuffed = true;
            }
        }
    }
}
