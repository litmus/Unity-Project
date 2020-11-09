using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private EngagementManager _engagementManager;

    void Start()
    {
        _engagementManager = GameObject.Find("EngagementsManager").GetComponent<EngagementManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if(collision.tag == "Player")
        {
            _engagementManager.HolidaySeason();
        }
    }
}
