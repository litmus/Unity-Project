using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engagement : MonoBehaviour
{
    private GameManager gameManager;
    private EngagementManager engagementManager;
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        engagementManager = GameObject.Find("EngagementsManager").GetComponent<EngagementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Random.Range(0.1f, 10.0f);
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            Destroy(this.gameObject);
            gameManager.AddPoints(engagementManager.engagementPoints);
        }
    }


}
