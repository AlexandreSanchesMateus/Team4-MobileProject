using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptTest : MonoBehaviour
{
    private float horizontal;
    public float speed = 7f;
    private Vector3 vecRef = Vector3.zero;
    [Range(0f, 0.3f)] [SerializeField] private float smooth = 0.1f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        
        Movement(horizontal * speed * Time.fixedDeltaTime);
    }
    void Movement(float velocity)
    {
        if (rb.gravityScale != 0f)
        {
            Vector3 Move = new Vector2(velocity * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Move, ref vecRef, smooth);
            
        }
    }
}
