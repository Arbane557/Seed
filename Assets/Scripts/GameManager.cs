using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countDown;
    [SerializeField]
    private float countdownTimer;
    public bool WaveStart;
    private int waveNum;
    [SerializeField]
    private int waveDuration;
    [SerializeField]
    private List<AudioClip> BGM = new List<AudioClip>();
    [SerializeField]
    private AudioSource audioSource;
    public Animator animator;
    private void Start()
    {
        waveNum = 0;
        WaveStart = false;
    }
    private void Update()
    {
        countdownTimer -= 1 * Time.deltaTime;
        countDown.text = "Wave " + waveNum + ", " + Mathf.RoundToInt(countdownTimer) + " left";
        
        if(countdownTimer <0 )
        {      
            StartCoroutine(changeBGM());    
            if (!WaveStart)
            {
                waveNum++;
                countdownTimer = waveDuration;
                WaveStart = true;
            }
            else
            {
                waveNum++;
                countdownTimer = waveDuration;
                WaveStart = false;
            }
        }
    }
    IEnumerator changeBGM()
    {
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        audioSource.clip = BGM[waveNum%2];

    }
}
