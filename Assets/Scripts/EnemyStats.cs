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
    public IEnumerator takeDamage(float damage)
    {
        health -= damage;

        yield return new WaitForSeconds(0.5f);

    }
}
