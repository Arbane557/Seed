using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ObjNearPlayer = new List<GameObject>();

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction interact;

    public bool isPlayer1;
    public bool isPlayer2;
    [SerializeField]
    private bool isOnField;
    [SerializeField]
    private bool isOnSource;
    public bool isInteract;
    public bool isDead;
    private Rigidbody rb;
    [SerializeField]
    public float movementForce = 1f;
    public float closestObjDist;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float radius = 3f;
    [SerializeField]
    private float maxdist = 0f;
    [SerializeField]
    private LayerMask layermask;
    public float currDist;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    public Transform camTransform;
    [SerializeField]
    private GameObject seedObj;
    [SerializeField]
    private GameObject currInteracted;
    public GameObject dropZone;
    private GameManager gameManager;
    private void Awake()
    {       
        rb = this.GetComponent<Rigidbody>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        move = player.FindAction("Movement");
        interact = player.FindAction("Interact");
        player.FindAction("Interact").Enable();
        player.Enable();
    }

    private void OnDisable()
    {       
        player.Disable();
    }

    private void FixedUpdate()
    {
        isDead = gameObject.GetComponent<PlayerStats>().isDead;
        if (!isDead)
        {
            transform.parent.GetChild(1).GetComponent<InputHandler>().horizontal = player.FindAction("Look");
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
            foreach (GameObject item in ObjNearPlayer)
            {
                if (item.CompareTag("Interactable"))
                {
                    item.GetComponent<Interactable>().EnableInteract();
                }
            }
            ObjNearPlayer.Clear();
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(transform.position, radius, transform.forward, maxdist, layermask, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit item in hit)
            {
                if (item.transform.gameObject.CompareTag("Interactable"))
                {
                    ObjNearPlayer.Add(item.transform.gameObject);
                }
            }
            player.FindAction("Interact").performed += grab;
            player.FindAction("Menu").performed += gameManager.pause;
            if (ObjNearPlayer != null)
            {
                closestObjDist = 10f;
                foreach (GameObject item in ObjNearPlayer)
                {
                    item.GetComponent<Interactable>().EnableInteract();
                    if (!isInteract)
                    {
                        if (Vector3.Distance(this.transform.position, item.transform.position) < closestObjDist)
                        {
                            currInteracted = item.gameObject;
                            closestObjDist = Vector3.Distance(this.transform.position, item.transform.position);
                        }
                    }
                }
            }
            if (!isInteract)
            {
                if (ObjNearPlayer.Count < 1)
                {
                    currInteracted = null;
                }
            }

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f)
                rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
                rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
            LookAt();
        }
       
    }
    private void grab(InputAction.CallbackContext context)
    {
        if (!isInteract)
        {
            currInteracted.transform.position = dropZone.transform.position;
            currInteracted.transform.parent = transform;
            isInteract = true;
        }
        else
        {
            currInteracted.transform.position = dropZone.transform.position;
            currInteracted.transform.SetParent(null);
            isInteract = false;
        }
    }
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            isOnField = true;
            isOnSource = false;
        }
        if (other.CompareTag("Source"))
        {
            isOnSource = true;
            isOnField = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

   

}