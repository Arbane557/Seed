using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject InteractUI;
    private bool IsInteractable;
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
