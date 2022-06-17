using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : MonoBehaviour
{
    public Sprite open;
    public Sprite closed;

    public GameObject flamme;
    public GameObject neige;

    private SpriteRenderer sr;
    public bool isOpen;

    private AudioSource allume;
    public AudioSource constant;

    public Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        allume = gameObject.GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        //sr.sprite = closed;
    }

    private void OnMouseDown()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {
            Debug.Log("Change sprite");

            if (!isOpen)
            {
                flamme.gameObject.SetActive(true);
                neige.gameObject.SetActive(false);
                allume.Play();
                StartCoroutine("fire");
                animator.SetBool("LightTorch", true);
            }

            

        } 


    }

    private IEnumerator fire()
    {
        yield return new WaitForSeconds(1f);
        constant.Play();
    }



}
