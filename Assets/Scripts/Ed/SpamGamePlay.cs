using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpamGamePlay : MonoBehaviour
{
    public bool IsGameRunning = false;

    public int MaxProjectiles = 2;
    public int NumberOfEnemies = 20;

    public GameObject projectile;
    public GameObject startGameLabel;

    public GameObject spam;

    public int NumberOfProjectiles => GameObject.FindGameObjectsWithTag("Projectile").Length;

    public int NumberOfSpamsInPlay => GameObject.FindGameObjectsWithTag("Enemy").Length;

    public int Score 
    { 
        get 
        {
            return int.Parse(GameObject.FindGameObjectsWithTag("Score")[0].GetComponent<Text>().text);
        }
        set 
        {
            GameObject.FindGameObjectsWithTag("Score")[0].GetComponent<Text>().text = value.ToString();
        }
    }

    private MailyPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Maily").GetComponent<MailyPlayer>();
        startGameLabel = GameObject.FindGameObjectsWithTag("StartGameLabel")[0];
    }

    // Update is called once per frame
    void Update()
    {

        startGameLabel.active = !IsGameRunning;

        if (Input.GetKeyDown("escape"))
        {
            IsGameRunning = false;
            SceneManager.LoadScene("Overworld Scene");
        }


        //do running game logic here
        if(IsGameRunning)
        {
            if (Input.GetKeyDown("space") && NumberOfProjectiles < MaxProjectiles)
            {
                Fire();
            }

            if(NumberOfSpamsInPlay == 0)
            {
                AddSpam();
            }
        }


        if (Input.GetKeyDown("space") && !IsGameRunning)
        {
            IsGameRunning = true;
            SetUp();
        }


    }

    void SetUp()
    {
        Score = 0;
        player.GetComponent<MailyPlayer>().ResetToHomePosition();
        RemoveAllExistingSpam();
        AddSpam();
    }

    void RemoveAllExistingSpam()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void AddSpam()
    {
        var lastAddedPosition = spam.transform.position;
        int xOffset = 2;
        int yOffset = 0;

        for (int i = 0; i < NumberOfEnemies; i++)
        {
            var newSpam = Instantiate(spam);
            if (i == 10)
            {
                yOffset = -2;
                lastAddedPosition = spam.transform.position;
            }
            newSpam.transform.position = new Vector3(lastAddedPosition.x + 2, newSpam.transform.position.y + yOffset);
            lastAddedPosition = newSpam.transform.position;
        }
    }

    public void GameOver()
    {
        IsGameRunning = false;

        var projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var projectile in projectiles)
        {
            Destroy(projectile);
        }

    }

    void Fire()
    {
        // have projectile come out of maily
        projectile.transform.position = player.transform.position;

        Instantiate(projectile);
    }

    
}
