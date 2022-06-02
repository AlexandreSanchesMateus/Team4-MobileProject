using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{
    // Int / float

    public float speed = 1;

    // Gameobject
    public GameObject depart1;
    public GameObject depart2;

    // Class
    public PlayerScriptTest playerMov;
    

    // Booleans
    public bool elevating = false;
    public bool swap = false;

    public override void Interact()
    {
        Debug.Log("Interaction avec l'échelle");
        playerMov.GetComponent<PlayerScriptTest>().enabled = false;
        playerMov.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        playerMov.GetComponent<BoxCollider2D>().isTrigger = true;
        playerMov.GetComponent<Rigidbody2D>().gravityScale = 0f;
        playerMov.GetComponent<PlayerIntPreset>().enabled = false;
        
        elevating = true;

        if(swap == false)
            playerMov.transform.position = depart1.transform.position;
                    
        else
            playerMov.transform.position = depart2.transform.position;

        swap = !swap;
    }

    private void Update()
    {
        if(elevating == true)
        {
            if(swap == true)
            playerMov.transform.position = playerMov.transform.position + new Vector3(0, speed ,0);

            else
            playerMov.transform.position = playerMov.transform.position - new Vector3(0, speed , 0);
        }
    }
}
