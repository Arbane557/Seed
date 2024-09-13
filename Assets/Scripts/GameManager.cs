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
    [SerializeField]
    private Animator animator;
    public bool isStarted;
    private void Start()
    {
        waveNum = 1;
        WaveStart = false;
        countdownTimer = waveDuration;
    }
    private void Update()
    {
        if (isStarted)
        {
            countdownTimer -= 1 * Time.deltaTime;
            countDown.text = "Wave " + waveNum + ", " + Mathf.RoundToInt(countdownTimer) + " left";

            if (countdownTimer < 2)
            {
                StartCoroutine(changeBGM());
                if (countdownTimer < 1)
                {
                    waveNum++;
                    countdownTimer = waveDuration;

                    if (!WaveStart) WaveStart = true;
                    else WaveStart = false;
                }
            }
        }
    }
    IEnumerator changeBGM()
    {
        animator.SetInteger("Fade",1);
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = BGM[waveNum%2];
        audioSource.Play();
        animator.SetInteger("Fade", 0);
    }
}
