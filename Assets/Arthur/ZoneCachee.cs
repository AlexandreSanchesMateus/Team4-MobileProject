using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCachee : MonoBehaviour
{
    public Transform end;
    public Transform back;
    public GameObject secret;

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            secret.transform.position = end.position;
        }
    }
    void OnTriggerExit2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            secret.transform.position = back.position;
        }
    }


}


//private SpriteRenderer spriteRenderer;
//dans ton start:
//spriteRenderer = GetComponent<SpriteRenderer>();
//dans un onTriggerEnter2D:
//spriteRenderer.Enabled = false;
//et dans un un onTriggerExit2D:
//spriteRenderer.enabled = true;