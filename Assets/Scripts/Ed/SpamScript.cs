using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamScript : MonoBehaviour
{
    SpamGamePlay spamGamePlay;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        spamGamePlay = Camera.main.GetComponent<SpamGamePlay>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(spamGamePlay.IsGameRunning)
        {
            transform.Translate(new Vector3(0, -.2f, 0) * 3 * Time.deltaTime);
        }
    
    }

    //if item is offscreen destroy
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: score points        
        Destroy(this.gameObject);
        Debug.Log("collided with " + collision.gameObject.tag);
        if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);

            spamGamePlay.Score += 10;
        }
        else
        {
            spamGamePlay.GameOver();
        }
    }
}
