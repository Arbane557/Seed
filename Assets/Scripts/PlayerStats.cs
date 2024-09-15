using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public float currHP;
    [SerializeField]
    private float maxHP = 100f;
    public bool isDead;
    public bool isGameOver;
    [SerializeField]
    private GameObject healthUI1;
    [SerializeField]
    private GameObject healthUI2;
    [SerializeField]
    private GameObject HealthUI;
    public Slider sl;
    public Transform camTransform;
    private ThirdPersonController TPC;
        [SerializeField]
    private GameObject respawnUI;
    [SerializeField]
    private TextMeshProUGUI countDown;
    private List<LayerMask> UILayers;
    private float respawnTime = 10;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        TPC = gameObject.GetComponent<ThirdPersonController>();
        if(TPC != null )
        camTransform = TPC.camTransform;
        currHP = maxHP;

        if (TPC != null)
        {
            if (TPC.isPlayer1) HealthUI = Instantiate(healthUI1, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            else if (TPC.isPlayer2) HealthUI = Instantiate(healthUI2, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
        countDown = HealthUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        countDown.gameObject.SetActive(false);
    }
    void Update()
    {
       
        HealthUI.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        HealthUI.transform.LookAt(camTransform);
        sl = HealthUI.transform.GetChild(0).GetComponent<Slider>();
        sl.value = currHP / maxHP;

        if (currHP <= 0)
        {
            if (this.CompareTag("Main Tree"))
            {
                gm.isLost = true;
            }
            else
            {
                if (TPC != null) isDead = true;
                else isGameOver = true;
            }
        }
        if(TPC != null)
        TPC.isDead = isDead;
        if (isDead)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            countDown.gameObject.SetActive(true);
            countDown.text = "Respawning in " + Mathf.RoundToInt(respawnTime);
            respawnTime -= Time.deltaTime;
            if (respawnTime <= 0)
            {
                countDown.gameObject.SetActive(false);
                currHP = maxHP;
                isDead = false;
                respawnTime = 10;
                if (TPC != null)
                TPC.isDead = isDead;
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public void damage(float damage)
    {
        currHP -= damage;
    }
}
