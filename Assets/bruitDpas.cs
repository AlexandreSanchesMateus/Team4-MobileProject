using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bruitDpas : MonoBehaviour
{
    private bool init = false;
    private bool canJump;
    private Vector2 direction;
    [SerializeField] private AudioSource bruitDePas;

   

    private void FixedUpdate()
    {
        canJump = gameObject.GetComponent<PlayerMovement2>().canJump;
        direction = gameObject.GetComponent<PlayerMovement2>().direction;

        if (direction.magnitude > 0.1f)
        {
            Debug.Log("BITE");

            if (canJump)
            {
                if (init == false)
                {

                    bruitDePas.Play();
                    init = true;
                }

            }
            else
                if (init)
            {

                bruitDePas.Pause();
                init = false;
            }


        }
        else
        {
            bruitDePas.Pause();
            init = false;
        }

    }


}
