using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class OnToggle : MonoBehaviour
{
    private PlayerInputManager m_PlayerInputManager;
    private EnemySpawner m_EnemySpawner;
    [SerializeField]
    private GameObject mc;
    private GameManager gm;
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject UItutorial;
    private float i;
    [SerializeField]
    private TextMeshProUGUI lostCountDown;
    private bool isDone;
    // Start is called before the first frame update
    void Awake()
    {
        i = 10;
        m_PlayerInputManager = FindObjectOfType<PlayerInputManager>();
        m_EnemySpawner = FindObjectOfType<EnemySpawner>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();   
    }
    private void Update()
    {
        if (isDone)
        {
            lostCountDown.gameObject.SetActive(true);
            i -= Time.deltaTime;
            lostCountDown.text = "Returning in " + Mathf.RoundToInt(i);
            if(i < 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gm.isWin)
        {
            mc.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true); 
            title.gameObject.SetActive(true);
            title.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Tree Lives For Another Day...";
            transform.Rotate(0, 15f * Time.deltaTime, 0);
            isDone = true;
        }
        if (gm.isLost)
        {
            mc.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            title.gameObject.SetActive(true);
            title.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Tree Perished And You Failed...";
            transform.Rotate(0, 15f * Time.deltaTime, 0);
            isDone = true;
        }
        transform.Rotate(0, 15f * Time.deltaTime, 0);
    }
    private void OnEnable()
    {
        m_PlayerInputManager.onPlayerJoined += ToggleThis;
    }
    private void OnDisable()
    {
        m_PlayerInputManager.onPlayerJoined -= ToggleThis;
    }
    private void ToggleThis(PlayerInput player)
    {
        UItutorial.SetActive(true);
        title.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public void playStart()
    {
        Time.timeScale = 1;
        UItutorial.gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        gm.isStarted = true;
        Destroy(title.transform.GetChild(1).gameObject);
        mc.SetActive(true);
    }

}
