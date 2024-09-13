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
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>(); 
        waveNum = 0;
        WaveStart = false;
        countdownTimer = waveDuration;
        changeBGM();
    }
    private void Update()
    {
        countdownTimer -= 1 * Time.deltaTime;
        countDown.text = "Wave " + waveNum + ", " + Mathf.RoundToInt(countdownTimer) + " left";
        
        if(countdownTimer < 1 )
        {
            waveNum++;
            changeBGM();
            countdownTimer = waveDuration;
            if (!WaveStart) WaveStart = true;     
            else WaveStart = false;      
        }
    }
    private void changeBGM()
    {
        animator.SetInteger("Fade",1);
        //audioSource.clip = BGM[waveNum%2];
        //audioSource.Play();
        //animator.SetBool("Change", false);
    }
}
