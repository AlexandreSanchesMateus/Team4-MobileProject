using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform Lieu;
    public GameObject player;
    public GameObject Spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void teleport()
    {
        player.transform.position = Lieu.position;
    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            Spawn.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}