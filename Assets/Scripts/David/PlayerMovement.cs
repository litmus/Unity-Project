using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float speed = 100f;
    private Rigidbody2D rb2d;
    private LayerMask groundLayer;
    private int jumping = 0;
    private bool jumpPressed = false;
    private Vector2 velocity;
    
    public GameObject score;
    private TextMesh scoreMesh;
    private int issuesSquashed = 0;

    public GameObject leftWall;
    public GameObject rightWall;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        rb2d.freezeRotation = true;

        groundLayer = LayerMask.GetMask("Ground");

        scoreMesh = score.GetComponent("TextMesh") as TextMesh;
    }

    void Update()
    {
        if (Input.GetAxisRaw ("Cancel") > 0) {
            SceneManager.LoadScene("Overworld Scene");
        }

        float moveHorizontal = Input.GetAxisRaw ("Horizontal");

        float moveVertical = Input.GetAxisRaw ("Jump");
        float jump = 0f;

        if (moveVertical > 0) {
            if (!jumpPressed) {
                jumpPressed = true;

                RaycastHit2D hit2D = Physics2D.Raycast(rb2d.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, groundLayer);
                RaycastHit2D hit2DRight = Physics2D.Raycast(rb2d.position - new Vector2(-0.5f, 0.5f), Vector2.down, 0.2f, groundLayer);
                RaycastHit2D hit2DLeft = Physics2D.Raycast(rb2d.position - new Vector2(0.5f, 0.5f), Vector2.down, 0.2f, groundLayer);
                
                if (hit2D || hit2DRight || hit2DLeft) {
                    jumping = 10;
                }
            }
        } else {
            jumpPressed = false;
        }

        if (jumping > 0) {
            if (moveVertical > 0) {
                jumping--;
            } else {
                jumping -= 2;
            }
            jump = 3;
        }

        Vector2 movement = new Vector2 (moveHorizontal, jump);

        rb2d.AddForce (movement * speed);

        Vector3 wallv = leftWall.transform.position;
        leftWall.transform.position = new Vector3(wallv.x, transform.position.y, wallv.z);
        wallv = rightWall.transform.position;
        rightWall.transform.position = new Vector3(wallv.x, transform.position.y, wallv.z);

        velocity = rb2d.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Issue") {
            IssueController ic = collision.gameObject.GetComponent<IssueController>();
            
            if (velocity.y < 0) {
                if (ic && !ic.dying) {
                    ic.RemoveIssue();

                    issuesSquashed++;
                    scoreMesh.text = issuesSquashed + " / 20";

                    jumping = 5;
                }
            }
        }
    }
}