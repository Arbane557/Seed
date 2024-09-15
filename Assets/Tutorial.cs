using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> plants = new List<GameObject>();
    private int i = 0;
    private void OnEnable()
    {
        foreach (GameObject p in plants) { p.SetActive(false); }
        i = 0;
        plants[i].SetActive(true);
    }
    public void changePanel(bool direction)
    {
        if(direction == true)
        {
            if (i < 5)
            {
                plants[i].SetActive(false);
                i++;
                plants[i].SetActive(true);
            }
        }
        else
        {
            if (i > 0)
            {
                plants[i].SetActive(false);
                i--;
                plants[i].SetActive(true);
            }
        }
    }
   
}
