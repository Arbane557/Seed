using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject InteractUI;
    private bool IsInteractable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableInteract()
    {
        if (!IsInteractable)
        {
            InteractUI.gameObject.SetActive(true);
            IsInteractable = true;
        }
        else
        {
            InteractUI.gameObject.SetActive(false);
            IsInteractable = false;
        }
    }
}
