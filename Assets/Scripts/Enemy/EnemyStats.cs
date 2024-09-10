using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float damage)
    {
        health -= damage;
    }
}
