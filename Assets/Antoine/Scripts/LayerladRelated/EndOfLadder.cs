using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLadder : MonoBehaviour
{
    public GameObject otherArrivee;
    public PlayerScriptTest player;
    public PlayerIntPreset preset;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Arrive");
            player.onLadder = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

           

            gameObject.SetActive(false);
            otherArrivee.SetActive(true);
            
        }
    }
}
