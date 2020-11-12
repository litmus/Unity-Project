using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameRunning)
        {
            return;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        

        transform.Translate(new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime);

        if (transform.position.x >= 9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= -1) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }
}
