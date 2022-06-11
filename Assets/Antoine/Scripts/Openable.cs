using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : MonoBehaviour
{
    public Sprite open;
    public Sprite closed;

    public GameObject flamme;
    public GameObject neige;

    private SpriteRenderer sr;
    public bool isOpen;
   

    private void OnMouseDown()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {
            Debug.Log("Change sprite");

            if (!isOpen)
            {
                flamme.gameObject.SetActive(true);
                neige.gameObject.SetActive(false);
            }

            

        } 


    }


    private void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        
        //sr.sprite = closed;
    }

   
}
