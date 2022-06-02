using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfElevator : MonoBehaviour
{
    public Elevator elevator;
    public GameObject otherArrivee;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Arrivé");
            elevator.playerMov.GetComponent<PlayerScriptTest>().enabled = true;
            elevator.playerMov.rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            elevator.playerMov.GetComponent<BoxCollider2D>().isTrigger = false;
            elevator.playerMov.GetComponent<Rigidbody2D>().gravityScale = 1f;
            elevator.playerMov.GetComponent<PlayerIntPreset>().enabled = true;
            
            elevator.elevating = false;
            gameObject.SetActive(false);
            otherArrivee.SetActive(true);
        }
    }
}
