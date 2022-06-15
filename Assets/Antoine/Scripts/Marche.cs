using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marche : MonoBehaviour
{
    public Elevator elevator;

    public bool march1;
    public bool march2;
    public bool march3;
    public bool march4;

    public int number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (number == 1)
            {
                elevator.march1 = true;
                gameObject.SetActive(false);
            }


            else if (number == 2)
            {
                elevator.march2 = true;
                gameObject.SetActive(false);
            }


            else if (number == 3)
            {
                elevator.march3 = true;
                gameObject.SetActive(false);
            }


            else if (number == 4)
            {
                elevator.march1 = true;
                gameObject.SetActive(false);
            }
        }
        
            
    }
}
