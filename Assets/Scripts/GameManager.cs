using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI countDown;
    [SerializeField]
    private float countdownTimer;
    public bool WaveStart;
    [SerializeField]
    private int waveNum;
    [SerializeField]
    private int waveDuration;
    [SerializeField]
    private List<AudioClip> BGM = new List<AudioClip>();
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Animator animator;
    public bool isStarted;
    [SerializeField]
    private GameObject MenuUI;
    [SerializeField]
    private bool isSetting;
    public bool isWin;
    public bool isLost;
    private bool isPaused;
   
    private void Start()
    {
        waveNum = 1;
        WaveStart = false;
        countdownTimer = waveDuration;
    }
    private void Update()
    {

        if (waveNum == 15) { isWin = true; WaveStart = false; }
        if(isWin == true)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            foreach (var item in player)
            {
                Destroy(item.gameObject);
            }
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var item in player)
            {
                Destroy(item.gameObject);
            }
        }
        if (isStarted)
        {
            countdownTimer -= 1 * Time.deltaTime;
            countDown.text = "Wave " + waveNum + "       " + Mathf.RoundToInt(countdownTimer) + " left";

            if (countdownTimer < 1)
            {
                if (!isSetting)
                {
                    waveNum++;
                    StartCoroutine(changeBGM());
                }
            }
        }
    }
    IEnumerator changeBGM()
    {
        isSetting = true;
        animator.SetInteger("Fade",1);
        yield return new WaitForSeconds(1f);
        audioSource.clip = BGM[waveNum%2];
        audioSource.Play();
        animator.SetInteger("Fade", 0);
        countdownTimer = waveDuration;
        if (!WaveStart) WaveStart = true;
        else WaveStart = false;
        isSetting = false;
    }
    public void pause(InputAction.CallbackContext context)
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            MenuUI.SetActive(true);
            isPaused = true;
        }else
        {
            Time.timeScale = 1;
            MenuUI.SetActive(false);
            isPaused = false;
        }
    }
    public void returnMenu()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }

}
