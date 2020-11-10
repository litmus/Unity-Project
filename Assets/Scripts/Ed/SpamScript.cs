using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpamScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: score points
        
        Destroy(this.gameObject);
        Destroy(collision.gameObject);
    }
}
