using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    public float speed = 1;

    public GameObject depart1;
    public GameObject depart2;
    
    
    public PlayerMovement2 player;
    public PlayerIntPreset preset;

    public bool elevating;
    public bool swap;

    
    // A changer avec le vrai player
    public override void Interact()
    {
       
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Interaction avec l'échelle");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player avec l'échelle");
            PlayerMovement2.Instance.playerMovementEnable = false;
            player._rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<CapsuleCollider2D>().isTrigger = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;


            elevating = true;

            if (swap == false)
                player.transform.position = depart1.transform.position;

            else
                player.transform.position = depart2.transform.position;

            swap = !swap;
        }
    }

    private void Update()
    {
        if (elevating == true)
        {
             if(swap == true)
              {
            player.transform.position = player.transform.position + new Vector3(0, speed, 0);
            GyroManager.Instance.isGyroEnable = false;
             }



             else           
            player.transform.position = player.transform.position - new Vector3(0, speed , 0);


        }
    }

}
