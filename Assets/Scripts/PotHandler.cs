using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class PotHandler : MonoBehaviour
{
    public List<GameObject> seeds = new List<GameObject>();
    [SerializeField]
    private TextMeshProUGUI countUI;
    [SerializeField]
    private GameObject InteractUI;
    private bool isReady;
    private bool IsCharging;
    private float progress;
    private GameObject holder;
    [SerializeField]
    private List<GameObject> plantPrefab = new List<GameObject>();
    private Vector3 potPos;
    [SerializeField]
    private GameObject potPrefab;
    private bool isPlanted;
    private void Start()
    {
        seeds.Clear();
        potPos = transform.position;
        transform.GetChild(2).gameObject.SetActive(true);
    }
    private void Update()
    {
        InteractUI.GetComponentInChildren<Slider>().value = progress / 6;
        countUI.text = "" + seeds.Count + "/5";
        foreach (GameObject go in seeds)
        {
            go.SetActive(false);
        }       
        if(IsCharging)
        {
            InteractUI.SetActive(true);
            progress += 1 * Time.deltaTime;
        }
        else InteractUI.SetActive(false);
        if (progress > 6)
        {
            if (!isReady)
            {
                InteractUI.SetActive(false);
                holder.GetComponent<ThirdPersonController>().isInteract = true;
                transform.GetChild(2).gameObject.SetActive(false);
                IsCharging = false;
                progress = 0;
                isReady = true;
            }
            else
            {
                var pot = Instantiate(potPrefab, potPos, Quaternion.identity);
                pot.transform.Rotate(0, 180, 0);
                GameObject plantSpawn = Instantiate(plantPrefab[seeds.Count - 1], transform.position, Quaternion.identity);
                plantSpawn.transform.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
                plantSpawn.transform.SetParent(transform);
                holder.GetComponent<ThirdPersonController>().isInteract = false;
                Destroy(countUI.gameObject);
                seeds.Clear();
                isReady = false;
                progress = 0;
                IsCharging = false;
                gameObject.tag = "Interactable";
                Destroy(transform.GetChild(5).gameObject);
                this.GetComponent<Rigidbody>().isKinematic = false;
                isPlanted = false;
            }
        }
        if (isReady)
        {
            transform.position = holder.GetComponent<ThirdPersonController>().dropZone.transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            holder = other.gameObject;
            if (seeds.Count > 0)
            {
                Debug.Log("Player Deposit");
                IsCharging = true;
            }
        }
        if (other.gameObject.CompareTag("Station"))
        {
            if (isReady)
            {
                progress = 0;
                IsCharging = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsCharging = false;
            progress = 0;
        }
        if (other.gameObject.CompareTag("Station"))
        {
            IsCharging = false;
            progress = 0;
        }
    }

}
