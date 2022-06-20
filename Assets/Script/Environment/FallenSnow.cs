using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSnow : MonoBehaviour
{
    [SerializeField] private GameObject Spawn;
    private GameObject groundRef;
    private Sprite _sp;
    private Rigidbody2D _rb;

    private void Start()
    {
        groundRef = transform.GetChild(0).gameObject;
        _sp = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    groundRef.GetComponent<SpriteRenderer>().sprite = _sp;
        //    groundRef.GetComponent<BoxCollider2D>().enabled = true;
        //    transform.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        //    transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //}
        Spawn.transform.position = new Vector2(486.31f, 14.64f);
        if (collision.gameObject.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Collider2D _newCollider = gameObject.AddComponent<BoxCollider2D>();
            //_newCollider.isTrigger = false;
            _rb.gravityScale = 1;
            StartCoroutine("AddWeight");
        }
    }

    private IEnumerator AddWeight()
    {
    
        yield return new WaitForSeconds (1.5f);
        _rb.bodyType = RigidbodyType2D.Static;
    }
}
