using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buissons : MonoBehaviour
{
    public ParticleSystem Effet;
    public AudioSource Scratch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bush()
    {
        Effet.Play();
        Scratch.Play();
    }

}
