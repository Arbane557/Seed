using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnToggle : MonoBehaviour
{
    private PlayerInputManager m_PlayerInputManager;

    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerInputManager = FindObjectOfType<PlayerInputManager>();
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
    }

}
