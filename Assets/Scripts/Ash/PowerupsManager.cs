using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{
    [SerializeField] private GameObject _holidaySeasonPrefab;
    [SerializeField] private GameObject _testedInLitmusPrefab;
    [SerializeField] private GameObject _forwardToFriendsPrefab;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public IEnumerator SpawnRoutinePowerup()
    {
        yield return new WaitForSeconds(3.0f);

        while (gameManager.gameRunning)
        {
            int randomPowerupID = Random.Range(0, 3);
            Vector3 spawn = new Vector3(Random.Range(-9, 9), 0.0f);

            switch (randomPowerupID)
            {
                case 0:
                    Instantiate(_holidaySeasonPrefab, spawn, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(_testedInLitmusPrefab, spawn, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(_forwardToFriendsPrefab, spawn, Quaternion.identity);
                    break;
            }

            yield return new WaitForSeconds(Random.Range(4, 10));
        }
    }
}
