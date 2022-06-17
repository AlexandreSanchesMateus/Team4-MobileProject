using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSnowball : MonoBehaviour
{
    public GameObject block;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            collision.gameObject.transform.position = block.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
