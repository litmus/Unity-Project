using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    public float speed = 100f;
    private Rigidbody2D rb2d;
    private LayerMask groundLayer;
    private int jumping = 0;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        rb2d.freezeRotation = true;

        groundLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");

        float moveVertical = Input.GetAxisRaw ("Jump");
        float jump = 0f;

        if (moveVertical > 0) {
            RaycastHit2D hit2D = Physics2D.Raycast(rb2d.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, groundLayer);
            RaycastHit2D hit2DRight = Physics2D.Raycast(rb2d.position - new Vector2(-0.5f, 0.5f), Vector2.down, 0.2f, groundLayer);
            RaycastHit2D hit2DLeft = Physics2D.Raycast(rb2d.position - new Vector2(0.5f, 0.5f), Vector2.down, 0.2f, groundLayer);
            
            if (hit2D || hit2DRight || hit2DLeft) {
                jumping = 5;
            }
        }
        if (jumping > 0) {
            jumping--;
            jump = 4;
        }

        Vector2 movement = new Vector2 (moveHorizontal, jump);

        rb2d.AddForce (movement * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT!");
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }
    }
}