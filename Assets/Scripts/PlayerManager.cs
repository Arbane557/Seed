using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Users;
using UnityEngine.XR;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<GameObject> playerObj = new List<GameObject>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        
        Transform playerParent = player.transform.parent;
        playerObj.Add(playerParent.gameObject);
       
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask += playerLayers[players.Count];
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");

    }
    private void Update()
    {
        //setup for player 2 camera
        if (playerObj.Count > 1)
        {
            GameObject player2 = playerObj[1];
            ThirdPersonController tpc = player2.GetComponentInChildren<ThirdPersonController>();
            tpc.isPLayer2 = true;
            player2.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            tpc.movementForce = 5f;
            player2.transform.GetChild(0).GetComponent<CapsuleCollider>().isTrigger = true;
            player2.GetComponentInChildren<CinemachineFreeLook>().m_Orbits[1].m_Height = 25;
            player2.tag = null;
        }
    }

}