using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private EngagementManager _engagementManager;
    [SerializeField] private int powerupID;

    void Start()
    {
        _engagementManager = GameObject.Find("EngagementsManager").GetComponent<EngagementManager>();

        if(_engagementManager == null)
        {
            Debug.LogError("Engagement manager is missing");
        }
    }

    void Update()
    {
        Destroy(this.gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if(collision.tag == "Player")
        {
            switch (powerupID)
            {
                case 0:
                    _engagementManager.HolidaySeason();
                    break;
                case 1:
                    _engagementManager.TestedInLitmus();
                    break;
                case 2:
                    _engagementManager.ForwardToFriends();
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }
}
