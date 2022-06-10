using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttable : Interactable
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        spriteRenderer.color = Color.red;
    }
}
