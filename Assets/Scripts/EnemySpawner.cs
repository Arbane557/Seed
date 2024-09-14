using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float x;
    private float z;
    private float y;
    [SerializeField]
    private float spawnRate;
    private Vector3 spawnLoc;
    [SerializeField]
    private List<GameObject> enemyPrefab = new List<GameObject>();   
    [SerializeField]
    private LayerMask layermask;
    private GameManager gameManager;
    private bool Spawning;
    [SerializeField]
    private List<Transform> enemySpawnPoints = new List<Transform>();
    void Start()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
    }

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

        spawnLoc = new Vector3(x, y, z);
    }
    public IEnumerator spawnEnemy()
    {
        while (true)
        {
            //Debug.Log("Spawn");
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Count)]);
            randomSpawnLoc();
            enemy.transform.position = spawnLoc;
            yield return new WaitForSeconds(spawnRate);
        }
    }
    public void randomSpawnLoc()
    {
        int num =  Random.Range(0, enemySpawnPoints.Count);
        x = enemySpawnPoints[num].transform.position.x;
        z = enemySpawnPoints[num].transform.position.z;
        y = enemySpawnPoints[num].transform.position.y;
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(z);
        //RaycastHit[] hit;
        //hit = Physics.SphereCastAll(spawnLoc, 2f, transform.forward, 0, layermask, QueryTriggerInteraction.UseGlobal);
        //foreach (RaycastHit item in hit) 
        //{
        //    if (item.transform.gameObject.CompareTag("Enemy"))
        //    {
        //        randomSpawnLoc();
        //    }
        //}
    }
}
