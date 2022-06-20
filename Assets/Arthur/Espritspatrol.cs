using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspritParticle : MonoBehaviour
{
    public GameObject Effet;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            Effet.SetActive(true);
        }
    }
}
