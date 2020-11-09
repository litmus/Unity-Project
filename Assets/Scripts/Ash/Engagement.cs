using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Random.Range(1.0f, 10.0f);
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            Destroy(this.gameObject);

            // animation? breaking of the engagement
        }


        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Player Hit");

            // add to engagement
        }
    }


}
