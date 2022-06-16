using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLadder : MonoBehaviour
{
    public GameObject otherArrivee;
    public PlayerMovement2 player;
    public PlayerIntPreset preset;

    public Ladder ladder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Arrive");
            PlayerMovement2.Instance.playerMovementEnable = true;
            player._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            player._rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            player.GetComponent<CapsuleCollider2D>().isTrigger = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ladder.elevating = false;


            gameObject.SetActive(false);
            otherArrivee.SetActive(true);
            
            
        }
    }
}
