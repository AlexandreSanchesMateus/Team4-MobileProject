using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmthAppear : MonoBehaviour
{
    // GameObjects interactifs

    public GameObject objectToAppear;
   
    private void OnMouseDown()
    {
        objectToAppear.SetActive(true);
        Debug.Log("J'ai cliqué sur l'arbre");
    }
}
