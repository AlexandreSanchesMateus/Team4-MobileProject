using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;

    public Animator anim;
    public AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {

            part1.SetActive(true);
            part2.SetActive(true);
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            audio.Play();
            anim.SetTrigger("Cassage");

        }


    }
}
