using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CutRope()
    {
        spriteRenderer.color = Color.red;
    }
}
