using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private void Update()
    {
        CheckInteraction();
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<cuttable>())
                {
                    rc.transform.GetComponent<cuttable>().CutRope();
                    return;
                }
                else if (rc.transform.GetComponent<cuttable2>())
                {
                    rc.transform.GetComponent<cuttable2>().CutRope();
                    return;
                }
            }
        }
    }
}
