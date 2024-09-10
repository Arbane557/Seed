using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float x;
    private float z;
    [SerializeField]
    private float spawnRate;
    private Vector3 spawnLoc;
    [SerializeField]
    private List<GameObject> enemyPrefab = new List<GameObject>();   
    [SerializeField]
    private LayerMask layermask;
    private GameManager gameManager;
    private bool Spawning;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.WaveStart)
        {
            if (!Spawning)
            {
                StartCoroutine(spawnEnemy());
                Spawning = true;
            }
        }
        else
        {
            StopAllCoroutines();
            Spawning = false;
        }

        spawnLoc = new Vector3(x, 1, z);
    }

    public IEnumerator spawnEnemy()
    {
        while (true)
        {
            Debug.Log("Spawn");
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Count - 1)]);
            randomSpawnLoc();
            enemy.transform.position = spawnLoc;
            yield return new WaitForSeconds(spawnRate);
        }

    }

    public void randomSpawnLoc()
    {
        x = Random.Range(transform.position.x - 17, transform.position.x + 17);
        z = Random.Range(transform.position.z - 17, transform.position.z + 17);

        RaycastHit[] hit;
        hit = Physics.SphereCastAll(spawnLoc, 2f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit item in hit) 
        {
            if (item.transform.gameObject.CompareTag("Enemy"))
            {
                randomSpawnLoc();
            }
        }
    }
}
