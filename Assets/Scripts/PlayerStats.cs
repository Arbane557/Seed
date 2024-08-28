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
    private GameObject healthUI;
    private GameObject HealthUI;
    public Slider sl;
    public Transform camTransform;
    private ThirdPersonController TPC;
    void Start()
    {
        TPC = gameObject.GetComponent<ThirdPersonController>();
        camTransform = TPC.camTransform;
        currHP = maxHP;
        if (TPC.isPLayer2 == false)
        {
            HealthUI = Instantiate(healthUI, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            HealthUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        }
    }
    void Update()
    {
        if (TPC.isPLayer2 == false)
        {
            HealthUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            HealthUI.transform.LookAt(camTransform);
            sl = HealthUI.transform.GetChild(0).GetComponent<Slider>();
            sl.value = currHP / maxHP;
        }
    }

    public void damage(float damage)
    {
        currHP -= damage;
    }
}
