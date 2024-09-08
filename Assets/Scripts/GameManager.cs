using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countDown;
    private float countdownTimer;
    public bool WaveStart;
    private int waveNum;
    [SerializeField]
    private int waveDuration;
    private void Start()
    {
        waveNum = 0;
        WaveStart = false;
        countdownTimer = 10;
    }
    private void Update()
    {
        countdownTimer -= 1 * Time.deltaTime;
        countDown.text = "Wave " + waveNum + ", " + Mathf.RoundToInt(countdownTimer) + " left";
        
        if(countdownTimer <0 )
        {       
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
}
