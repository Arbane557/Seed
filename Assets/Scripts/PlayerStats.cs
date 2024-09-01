using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField, Range(0,100)]
    private float currHP;
    private float maxHP = 100f;

    [SerializeField]
    private GameObject healthUI1;
    [SerializeField]
    private GameObject healthUI2;
    private GameObject HealthUI;
    public Slider sl;
    public Transform camTransform;
    private ThirdPersonController TPC;
    [SerializeField]
    private List<LayerMask> UILayers;
    void Start()
    {
        TPC = gameObject.GetComponent<ThirdPersonController>();
        camTransform = TPC.camTransform;
        currHP = maxHP;

        if (TPC.isPlayer1)
        {
            //camTransform.GetComponent<Camera>().cullingMask -= UILayers[1];
            HealthUI = Instantiate(healthUI1, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
        if(TPC.isPlayer2) HealthUI = Instantiate(healthUI2, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        HealthUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

    }
    void Update()
    {
        HealthUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        HealthUI.transform.LookAt(camTransform);
        sl = HealthUI.transform.GetChild(0).GetComponent<Slider>();
        sl.value = currHP / maxHP;
    }

    public void damage(float damage)
    {
        currHP -= damage;
    }
}
