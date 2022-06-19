using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLimitation : MonoBehaviour
{
    public GameObject _targetCam;
    public GameObject Canvas;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Limite")
        {
            _targetCam.transform.position = PlayerMovement2.Instance.gameObject.transform.position;
        }

        if(truc.tag == "Noircissement")
        {
            InvokeRepeating("Noircissement", 1f, 1f);
        }
    }

    public void Noircissement()
    {
        Alpha = Alpha + 0.2f;
        if (alpha == 1f)
        {
            Canvas.GetComponent<canvasGroup>Alpha;
        }
    }
}
