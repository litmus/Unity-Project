using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagementManager : MonoBehaviour
{

    [SerializeField] private GameObject _engagementPrefab;
    [SerializeField] private GameObject _engagementsContainer;

    private float _waitPeriod = 2.0f;

    private GameManager gameManager;

    public int engagementPoints = 10;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public IEnumerator SpawnEngagement()
    {
        while (gameManager.gameRunning)
        {
            Vector3 spawn = new Vector3(Random.Range(-9, 9), 8);
            GameObject eng = Instantiate(_engagementPrefab, spawn, Quaternion.identity);
            eng.transform.parent = _engagementsContainer.transform;

            yield return new WaitForSeconds(_waitPeriod);
        }
    }

    public void TestedInLitmus()
    {
        engagementPoints = 50;

        StartCoroutine(TestedInLitmusTimer());
    }

    public void ForwardToFriends()
    {
        _waitPeriod = 0.5f;

        StartCoroutine(ForwardToFriendsTimer());
    }

    public void HolidaySeason()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 spawn = new Vector3(Random.Range(-9.0f, 9.0f), 8);
            GameObject eng = Instantiate(_engagementPrefab, spawn, Quaternion.identity);
            eng.transform.parent = _engagementsContainer.transform;
        }
    }

    private IEnumerator TestedInLitmusTimer()
    {
        yield return new WaitForSeconds(4);
        engagementPoints = 10;
    }

    private IEnumerator ForwardToFriendsTimer()
    {
        yield return new WaitForSeconds(4);
        _waitPeriod = 2.0f;
    }

}
