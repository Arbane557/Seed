using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private float timeToDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie += Time.deltaTime;
        if (timeToDie > 3.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.CompareTag("P1"))
        {
            if (collision.transform.gameObject.CompareTag("Player"))
            {
                collision.transform.gameObject.GetComponent<PlayerStats>().damage(damage);
                Destroy(this.gameObject);
            }
        }
        if (this.CompareTag("P2"))
        {
            if (collision.transform.gameObject.CompareTag("Enemy"))
            {
                collision.transform.gameObject.GetComponent<EnemyStats>().takeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
