using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagementManager : MonoBehaviour
{

    [SerializeField] private GameObject _engagementPrefab;
    [SerializeField] private GameObject _engagementsContainer;

    private bool _spawnEngagement = true;
    private float _waitPeriod = 3.0f;
    
    void Start()
    {
        StartCoroutine(SpawnEngagement());
    }

    private IEnumerator SpawnEngagement()
    {
        while (_spawnEngagement)
        {
            Vector3 spawn = new Vector3(Random.Range(-9, 9), 8);
            GameObject eng = Instantiate(_engagementPrefab, spawn, Quaternion.identity);
            eng.transform.parent = _engagementsContainer.transform;

            yield return new WaitForSeconds(_waitPeriod);
        }
    }

    public void TestedInLitmus()
    {
        // set engagement score to 2x
    }

    public void ForwardToFriends()
    {
        _waitPeriod = 1.0f;

        // Need to increase after 5s
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
}
