using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Cascade : Interactable
{
    public Sprite coule;
    public Sprite bloque;

    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        sr.sprite = bloque;
    }
    public override void Interact()
    {
        sr.sprite = coule;

        bc.enabled = false;
    }

   

}
