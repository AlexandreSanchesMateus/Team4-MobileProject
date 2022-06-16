using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Int / float

    public float speed = 1;
    

    // Gameobject
    public GameObject depart1;
    public GameObject depart2;

    // Class
    public PlayerMovement2 playerMov;
    

    // Booleans
    public bool elevating = false;
    public bool swap = false;

    public bool march1;
    public bool march2;
    public bool march3;
    public bool march4;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(march1 && march2 && march3 && march4)
        {
            Debug.Log("Interaction avec l'échelle");
            PlayerMovement2.Instance.playerMovementEnable = false;
            playerMov._rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            playerMov.GetComponent<CapsuleCollider2D>().isTrigger = true;
            playerMov.GetComponent<Rigidbody2D>().gravityScale = 0f;


            elevating = true;

                playerMov.transform.position = depart1.transform.position;
            //if (swap == false)

           // else
              //  playerMov.transform.position = depart2.transform.position;

           // swap = !swap;
        }
        
    }

    private void Update()
    {
        if(elevating == true)
        {
           // if(swap == true)
          //  {
            playerMov.transform.position = playerMov.transform.position + new Vector3(0, speed ,0);
            GyroManager.Instance.isGyroEnable = false;
           // }



           // else           
            //playerMov.transform.position = playerMov.transform.position - new Vector3(0, speed , 0);
                       
            
        }
    }
}
