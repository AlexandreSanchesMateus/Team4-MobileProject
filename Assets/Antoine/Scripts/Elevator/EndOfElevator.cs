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
            PlayerMovement2.Instance.playerMovementEnable = true;
            elevator.playerMov._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            elevator.playerMov._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            elevator.playerMov.GetComponent<CapsuleCollider2D>().isTrigger = false;
            elevator.playerMov.GetComponent<Rigidbody2D>().gravityScale = 1f;
            
            //elevator.playerMov.GetComponent<PlayerIntPreset>().enabled = true;
            
            elevator.elevating = false;
            gameObject.SetActive(false);
            otherArrivee.SetActive(true);

            if(GyroManager.Instance.isGyroEnable == false)
            GyroManager.Instance.isGyroEnable = true;
        }
    }
}
