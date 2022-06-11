using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public GameObject respawn;
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrapTrigger"))
            StartCoroutine("Respawn");
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cascade"))
        {
            StartCoroutine("Respawn");
            Debug.Log("Respawn");
        }
            
    }

    IEnumerator Respawn()
    {
        
        yield return new WaitForSeconds(1);
        
        transform.position = respawn.transform.position;
    }
}
