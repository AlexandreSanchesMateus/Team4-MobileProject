using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptTest : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    // A rajouter !
    public bool onLadder;

    public float speed = 7f;
    public float initSpeed;
    public float speedLad = 3f;
    public float initSpeedLad;

    //

    private Vector3 vecRef = Vector3.zero;
    [Range(0f, 0.3f)] [SerializeField] private float smooth = 0.1f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // A rajouter ! 
        onLadder = false;
        initSpeed = speed;
        initSpeedLad = speedLad;
        //  
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {       
        // A rajouter !

        if(onLadder == true)
        {
        GrindLadder(vertical * speedLad * Time.fixedDeltaTime);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;

        }

        else
        {
        Movement(horizontal * speed * Time.fixedDeltaTime);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
           gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            
        }
        //
    }
    void Movement(float velocity)
    {
        if (rb.gravityScale != 0f)
        {
            Vector3 Move = new Vector2(velocity * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Move, ref vecRef, smooth);
            
        }
    }

    void GrindLadder(float velocity)
    {
        if (rb.gravityScale != 100f)
        {
            Vector3 Move = new Vector2(rb.velocity.x, velocity* 10f);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Move, ref vecRef, smooth);

        }
    }
}
