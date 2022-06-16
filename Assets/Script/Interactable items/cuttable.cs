using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject log;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CutRope()
    {
        log.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine("AddWeight");
    }

    private IEnumerator AddWeight()
    {
        yield return new WaitForSeconds(1.5f);
        log.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
