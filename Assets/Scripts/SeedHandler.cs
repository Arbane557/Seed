using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedHandler : MonoBehaviour
{
    [SerializeField]
    private bool isCharging;
    public bool isReady;
    [SerializeField]
    private List<int> ingredientList = new List<int>();
    [SerializeField]
    private List<int> setIngredient = new List<int>();
    [SerializeField]
    private List<bool> progressBool = new List<bool>();
    [SerializeField]
    private int progressCount;
    [SerializeField]
    private float progress;
    [SerializeField]
    private GameObject popUpProgress;
    [SerializeField]
    private Transform camTransform1;
    [SerializeField]
    private Transform camTransform2;
    [SerializeField]
    private Material readyMats;
   
    [SerializeField]
    private List<GameObject> sign = new List<GameObject>();
    [SerializeField]
    private List<Color> mats = new List<Color>();
    private void Start()
    {
        setRandom();
        progressCount = 0;
    }
    void setRandom()
    {
        for (int i = 0; i < Random.Range(2, 4); i++) setIngredient.Add(ingredientList[Random.Range(0, ingredientList.Count)]);
        for (int i = 0; i < setIngredient.Count; i++) progressBool.Add(false);
        for (int i = 0; i < setIngredient.Count; i++)
        {
            sign[i].GetComponent<RawImage>().color = mats[setIngredient[i] - 1];
        }
    }
    private void Update()
    {
        isReady = isAllDone();
        if(GameObject.FindGameObjectWithTag("CAM") != null)
        camTransform1 = GameObject.FindGameObjectWithTag("CAM").transform;
        if (camTransform1 != null) popUpProgress.transform.LookAt(camTransform1.position);  
        popUpProgress.GetComponentInChildren<Slider>().value = progress / 10;
        if (!isReady)
        {
            if (!progressBool[progressCount])
            {
                if (isCharging)
                {
                    charge(true);
                }
                else
                {
                    popUpProgress.SetActive(false);
                }
            }
        }
        else
        {
            GetComponent<Renderer>().material = readyMats;
            foreach (GameObject item in sign)
            {
                item.GetComponent<RawImage>().color = Color.green;
            }
        }
        if(transform.parent != null)
        {
            isCharging = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Main Tree"))
        {
            other.gameObject.GetComponent<MainTree>().spawnedSeedObj.Add(this.gameObject);
            Debug.Log("Set");
        }
        if (other.gameObject.CompareTag("" + setIngredient[progressCount])) isCharging = true;
        else isCharging = false;
    }
    void charge(bool correctPos)
    {
        popUpProgress.SetActive(true);
        progress += 1 * Time.deltaTime;
        if(progress >= 10)
        {
            if (correctPos)
            {
                isCharging = false;
                progressBool[progressCount] = true;
                sign[progressCount].GetComponent<RawImage>().color = Color.green;
                progressCount++;
                progress = 0;
                popUpProgress.SetActive(false);
                isCharging = false;
            }
            //else
            //{
            //    Destroy(this.gameObject);
            //    progress = 0;
            //}
        }
    }
    private bool isAllDone()
    {
        for (int i = 0; i < progressBool.Count; i++)
        {
            if (progressBool[i] == false) return false;
        }   return true;
    }
}
