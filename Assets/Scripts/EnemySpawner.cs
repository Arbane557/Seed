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

    }
    public IEnumerator spawnEnemy()
    {
        while (true)
        {
            randomSpawnLoc();
            //Debug.Log("Spawn");
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Count)]);     
            enemy.transform.position = spawnLoc;
            yield return new WaitForSeconds(spawnRate);
        }
    }
    public void randomSpawnLoc()
    {
        int num =  Random.Range(0, enemySpawnPoints.Count);
        spawnLoc = new Vector3(enemySpawnPoints[num].transform.position.x, 
            enemySpawnPoints[num].transform.position.y, 
            enemySpawnPoints[num].transform.position.z);

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
