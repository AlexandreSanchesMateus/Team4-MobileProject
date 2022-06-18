using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform Lieu;
    public GameObject player;
    public GameObject Spawn;
    public GameObject Canvas;
    private float time;
    private Animator animator;
    public AudioSource monter;
    // Start is called before the first frame update
    void Start()
    {
        animator = Canvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void teleport()
    {
        StartCoroutine(Transi());
    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            Spawn.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    IEnumerator Transi()
    {
        animator.SetBool("Active", true);
        monter.Play();
        player.transform.position = Lieu.position;
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Active", false);
    }
}