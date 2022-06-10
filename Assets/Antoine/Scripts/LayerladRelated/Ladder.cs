using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    public GameObject depart;
    public GameObject depart2;
    public GameObject layerCheck;
    
    public PlayerScriptTest player;
    public PlayerIntPreset preset;

    
    // A changer avec le vrai player
    public override void Interact()
    {
       
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Frite");
        layerCheck.SetActive(true);

        if (player.onLadder == false)
        {

            player.onLadder = true;

           /* if (preset.inLayer1)
                player.transform.position = depart.transform.position;

            if (preset.inLayer2)
                player.transform.position = depart2.transform.position;*/


        }

        else
            player.onLadder = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.onLadder = false;
        layerCheck.SetActive(false);
    }

}
