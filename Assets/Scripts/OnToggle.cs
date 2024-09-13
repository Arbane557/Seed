using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnToggle : MonoBehaviour
{
    private PlayerInputManager m_PlayerInputManager;
    private EnemySpawner m_EnemySpawner;
    [SerializeField]
    private GameObject mc;
    private GameManager gm;
    [SerializeField]
    private GameObject title;
    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerInputManager = FindObjectOfType<PlayerInputManager>();
        m_EnemySpawner = FindObjectOfType<EnemySpawner>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();   
    }
    private void Update()
    {
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
        this.gameObject.SetActive(false);
        gm.isStarted = true;
        Destroy(title.gameObject);
        mc.SetActive(true);
    }

}
