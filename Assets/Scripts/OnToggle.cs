using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnToggle : MonoBehaviour
{
    private PlayerInputManager m_PlayerInputManager;
    private EnemySpawner m_EnemySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerInputManager = FindObjectOfType<PlayerInputManager>();
        m_EnemySpawner = FindObjectOfType<EnemySpawner>();
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
        //StartCoroutine(m_EnemySpawner.spawnEnemy());
        this.gameObject.SetActive(false);
    }

}
