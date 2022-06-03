using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedObj : MonoBehaviour
{

    // GameObjects interactifs

    public GameObject bougie;

    // Booleans 

    public bool open;

    public void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(open == true)
        {
            bougie.SetActive(true);
            Debug.Log("Bougie allumée");
        }
    }
   
}
