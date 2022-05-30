using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    public GameObject depart;
    public PlayerScriptTest player;

    
    public override void Interact()
    {
        Debug.Log("Frite");

        if (player.onLadder == false)
        {
            
            player.onLadder = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            player.transform.position = depart.transform.position;

        }

        else
            player.onLadder = false;
    }

    
    
}
