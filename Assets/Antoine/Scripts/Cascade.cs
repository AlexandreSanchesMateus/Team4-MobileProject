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
    public bool isOpen;

    private void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        sr.sprite = bloque;
    }
    public override void Interact()
    {
        
        if (isOpen)
            sr.sprite = bloque;
        else
            sr.sprite = coule;

        bc.size = new Vector2(0.06f,bc.size.y );
        bc.offset = new Vector2(0.04f, bc.offset.y);

        isOpen = !isOpen;
    }

   

}
