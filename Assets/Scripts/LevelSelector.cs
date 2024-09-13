using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public float rotationDegreesPerSecond = 36f;
    public float rotationDegreesAmount = 72f;
    public float totalRotation = 0;
    private bool r;
    private bool l;
    private bool start;
    [SerializeField, Range(0,1)] float rotationProgress = 0;
    [SerializeField]
    private List<GameObject> worldButton = new List<GameObject>();
    [SerializeField]
    private List<string> sceneName = new List<string>();
    [SerializeField] private GameObject camTransform;
    private int i = 0;
    private Vector3 targetCamLoc;

    private void Start()
    {
        targetCamLoc = new Vector3(camTransform.transform.position.x, camTransform.transform.position.y, camTransform.transform.position.z + 10);
    }
    public void RotateRight()
    {
        if (!r)
        {
            rotationDegreesPerSecond = 72f;
            rotationDegreesAmount = 72f;
            totalRotation = 0;
            worldButton[i].SetActive(false);
            i++;
            if (i > 4) i = 0;
            worldButton[i].SetActive(true);
            r = true;
        }
    }
    public void RotateLeft() 
    {
        if (!l)
        {
            rotationDegreesPerSecond = -72f;
            rotationDegreesAmount = -72f;
            totalRotation = 0;
            worldButton[i].SetActive(false);
            i--;
            if (i < 0) i = 4;
            worldButton[i].SetActive(true);
            l = true;
        }
    }
    private void Update()
    {
        if(r || l){
            if (Mathf.Abs(totalRotation) < Mathf.Abs(rotationDegreesAmount)) SwingOpen();
            else{
                r = false; l = false;
            }
        } 
        if(start) camTransform.transform.position = Vector3.MoveTowards(camTransform.transform.position, targetCamLoc, 10f*Time.deltaTime);
        if(camTransform.transform.position.z >= targetCamLoc.z) SceneManager.LoadScene(sceneName[i]);
    }
    void SwingOpen()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation =
        Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
        totalRotation += Time.deltaTime * rotationDegreesPerSecond;
    }
    public void OpenWorld()
    {
        start = true;
        var bgm = GameObject.FindGameObjectWithTag("music");
        bgm.GetComponent<AudioSource>().volume -= Time.deltaTime;
    }
}
