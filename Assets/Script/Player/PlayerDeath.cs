using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public Transform respawn;
    private AudioSource splash;
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.CompareTag("TrapTrigger"))
             StartCoroutine("Respawn");
     }*/

    private void Start()
    {
        splash = gameObject.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cascade"))
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine("Respawn");
            Debug.Log("Respawn");
            splash.Play();
        }
            
    }

    IEnumerator Respawn()
    {
        
        yield return new WaitForSeconds(1);

        transform.position = respawn.position;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
