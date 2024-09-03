using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countDown;
    private int countdownTimer;
    private bool WaveStart;
    private int waveNum;
    private void Start()
    {
        waveNum = 0;
        WaveStart = false;
        countdownTimer = 10;
    }
    private void Update()
    {
        countdownTimer -= Mathf.FloorToInt(1 * Time.deltaTime);
        countDown.text = "Wave " + waveNum + ", " + countdownTimer + " left";
        
        if(countdownTimer <0 )
        {       
            if (!WaveStart)
            {
                waveNum++;
                countdownTimer = 10;
                WaveStart = true;
            }
            else
            {
                waveNum++;
                countdownTimer = 10;
                WaveStart = false;
            }
        }
    }
}
