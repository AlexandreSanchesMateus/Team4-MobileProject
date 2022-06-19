using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLimitation : MonoBehaviour
{
    public GameObject _targetCam;
    public GameObject Canvas;
    private float Alpha;
    // Start is called before the first frame update
    void Start()
    {
        Alpha = 0f;
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
            Debug.Log("Limite");
        }

        if(truc.tag == "Noircissement")
        {
            InvokeRepeating("Noir", 1f, 1f);
        }
    }

    public void Noir()
    {
        Alpha = Alpha + 0.2f;
        Canvas.GetComponent<CanvasGroup>().alpha = Alpha;
        if (Alpha == 1f)
        {
            _targetCam.transform.position = PlayerMovement2.Instance.gameObject.transform.position;
            Alpha = 0f;
        }
    }
}
