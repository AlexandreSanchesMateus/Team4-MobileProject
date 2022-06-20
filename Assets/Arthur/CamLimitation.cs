using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamLimitation : MonoBehaviour
{
    public GameObject _targetCam;
    public CanvasGroup Transi;
    public float Alpha;

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

        if (truc.tag == "Noircissement")
        {
            InvokeRepeating("Noir", 0.7f, 0.7f);
        }
    }

        void OnTriggerExit2D(Collider2D truc)
        {
            if (truc.tag == "Noircissement")
            {
                Alpha = 0f;
                Transi.alpha = Alpha;
                CancelInvoke();
            }
        }

    public void Noir()
    {
        Alpha = Alpha + 0.333333333333333333333333f;
        Transi.alpha = Alpha;
        if (Alpha >= 1f)
        {
            _targetCam.transform.position = PlayerMovement2.Instance.gameObject.transform.position;
            Alpha = 0f;
            Transi.alpha = Alpha;
            CancelInvoke();
        }
    }
}