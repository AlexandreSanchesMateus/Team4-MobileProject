using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    
   
    public GameObject Spawn;
        
    
    // Start is called before the first frame update
    [SerializeField] private Transform Lieu;
    [SerializeField] private int newLayerID = -1;
    [SerializeField] private AudioSource monter;

    private bool isPlayerInside;


    public void teleport()
    {
        if (isPlayerInside)
            LayerManager2.Instance.TransitionScreen(Lieu.position, newLayerID);
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            isPlayerInside = true;
            Spawn.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            isPlayerInside = false;
    }
   
}