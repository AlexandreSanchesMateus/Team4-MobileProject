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

            if (preset.inLayer1)
            {
                preset.inLayer2 = true;
                preset.inLayer1 = false;
            }
            else
            {
                preset.inLayer1 = true;
                preset.inLayer2 = false;
            }

            gameObject.SetActive(false);
            otherArrivee.SetActive(true);
            
        }
    }
}
