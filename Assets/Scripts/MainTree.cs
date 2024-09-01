using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : MonoBehaviour
{
    [SerializeField] 
    private List<Transform> spawnPoint;
    [SerializeField]
    private GameObject seed;
    [SerializeField] 
    public List<GameObject> spawnedSeedObj = new List<GameObject>();
    [SerializeField]
    private Transform pivot;
    private bool isSpawning;
    private void Start()
    {
        StartCoroutine(spawnSeed());
    }
    private void Update()
    {
        for (int i = 0; i < spawnedSeedObj.Count; i++)
        {
            if (spawnedSeedObj[i].transform.parent != null) spawnedSeedObj.Remove(spawnedSeedObj[i]);
            spawnedSeedObj[i].transform.position = spawnPoint[i].transform.position;
        }
        
        if(spawnedSeedObj.Count == 5)
        {
            StopAllCoroutines();
            isSpawning = false;
        }
        else
        {
            if (!isSpawning)
            {
                StartCoroutine(spawnSeed());
            }
        }
    }
    IEnumerator spawnSeed()
    {
        while (true)
        {
            isSpawning = true;
            GameObject spawnedSeed = Instantiate(seed, transform.position, Quaternion.identity);
            spawnedSeedObj.Add(spawnedSeed);
            yield return new WaitForSeconds(10f);
        }
    }
}

