using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Users;
using UnityEngine.XR;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> playersSpawns = new List<Transform>();
    [SerializeField]
    private List<GameObject> playerObj = new List<GameObject>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;
    [SerializeField]
    private List<LayerMask> UILayers;
    private PlayerInputManager playerInputManager;
    private bool setupdone1;
    private bool setupdone2;
    private bool isGameOver;
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
        playerParent.gameObject.transform.position = playersSpawns[playerObj.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask += playerLayers[players.Count];    
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
         
    }
    private void Update()
    {   
        if (playerObj.Count != 0)
        {
            if (!setupdone1)
            {
                GameObject player1 = playerObj[0];
                player1.gameObject.name = "player1";

                ThirdPersonController tpc1 = player1.GetComponentInChildren<ThirdPersonController>();
                tpc1.isPlayer1 = true;
                tpc1.camTransform = player1.transform.GetChild(1);
                player1.transform.GetChild(1).GetComponent<Camera>().cullingMask -= UILayers[1];
                setupdone1 = true;
            }
            if (!setupdone2)
            {
                if (playerObj.Count > 1)
                {
                    GameObject player2 = playerObj[1];
                    player2.gameObject.name = "player2";
                    
                    ThirdPersonController tpc2 = player2.GetComponentInChildren<ThirdPersonController>();
                    tpc2.camTransform.tag = "CAM2";
                    tpc2.isPlayer2 = true;
                    tpc2.camTransform = player2.transform.GetChild(1);
                    player2.transform.GetChild(1).GetComponent<Camera>().cullingMask -= UILayers[0];
                    player2.transform.GetChild(1).GetComponent<Camera>().cullingMask += UILayers[1];
                    setupdone2 = true;
                }
            }
        }
        foreach (var player in playerObj)
        {
            if (player.GetComponentInChildren<PlayerStats>().isDead == false) continue;
            else isGameOver = true;
        }
    }
}