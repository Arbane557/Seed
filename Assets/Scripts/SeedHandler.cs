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
    private GameObject popUpProgress2;
    [SerializeField]
    private Transform camTransform1;
    [SerializeField]
    private Transform camTransform2;
    [SerializeField]
    private Transform signPivot;
    [SerializeField]
    private Material readyMats;
    [SerializeField]
    private GameObject UIPref;
    private GameObject UI2;
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
        UI2 = Instantiate(UIPref,transform.position,Quaternion.identity);
        UI2.transform.SetParent(transform);
        UI2.transform.position = UIPref.transform.position;
        UI2.transform.localScale = UIPref.transform.localScale;
        UI2.layer = 8;
        popUpProgress2 = UI2.transform.GetChild(0).gameObject;
        popUpProgress2.layer = 8;
    }
    private void Update()
    {
        isReady = isAllDone();
        if(GameObject.FindGameObjectWithTag("CAM") != null)
            camTransform1 = GameObject.FindGameObjectWithTag("CAM").transform;
        if (GameObject.FindGameObjectWithTag("CAM2") != null)
            camTransform2 = GameObject.FindGameObjectWithTag("CAM2").transform;
        if (camTransform1 != null) { signPivot.transform.LookAt(camTransform1.position); }
        if (camTransform2 != null) { UI2.transform.LookAt(camTransform2.position); }

        popUpProgress.GetComponentInChildren<Slider>().value = progress / 10;
        popUpProgress2.GetComponentInChildren<Slider>().value = progress / 10;

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
                    popUpProgress2.SetActive(false);
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
       
        if (other.gameObject.CompareTag("" + setIngredient[progressCount])) isCharging = true;
        else isCharging = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("POT"))
        {
            if (isReady)
            {
                if(other.gameObject.GetComponent<PotHandler>().seeds.Count < 5)
                    other.gameObject.GetComponent<PotHandler>().seeds.Add(this.gameObject);

            }
            Debug.Log("Entered Pot");
        }
    }

    void charge(bool correctPos)
    {
        popUpProgress.SetActive(true);
        popUpProgress2.SetActive(true);
        progress += 1 * Time.deltaTime;
        if(progress >= 10)
        {
            if (correctPos)
            {
                isCharging = false;
                progressBool[progressCount] = true;
                sign[progressCount].GetComponent<RawImage>().color = Color.green;
                UI2.transform.GetChild(1).transform.GetChild(progressCount).GetComponent<RawImage>().color = Color.green;
                progressCount++;
                progress = 0;
                popUpProgress.SetActive(false);
                popUpProgress2.SetActive(false);
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
