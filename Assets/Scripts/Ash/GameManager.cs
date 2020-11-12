using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameRunning = false;
    private int gameTime = 31;
    private int score = 0;

    private EngagementManager engagementManager;
    private PowerupsManager powerupsManager;
    private UIManager uiManager;

    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private AudioSource lossSound;
    //[SerializeField] private AudioClip _winSound;

    void Start()
    {
        engagementManager = GameObject.Find("EngagementsManager").GetComponent<EngagementManager>();
        powerupsManager = GameObject.Find("PowerupsManager").GetComponent<PowerupsManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        score = 0;
        uiManager.UpdateScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Return) && gameRunning == false)
        {
            gameTime = 31;
            score = 0;
            gameRunning = true;
            StartCoroutine(engagementManager.SpawnEngagement());
            StartCoroutine(powerupsManager.SpawnRoutinePowerup());
            StartSoundtrack();
            hideUI();
            StartCoroutine(StartGame());
        }

        if (gameTime == 0 && gameRunning)
        {
            if(score < 500 && !lossSound.isPlaying)
            {
                lossSound.Play();
            }
            else
            {

            }

            gameRunning = false;
            showUI();
            StopSoundtrack();
        }
        
    }

    private void hideUI()
    {
        var objects = GameObject.FindGameObjectsWithTag("Pregame");
        foreach (GameObject obj in objects)
        {
            CanvasRenderer rend = obj.GetComponent<CanvasRenderer>();
            rend.SetAlpha(0);
        }
    }

    private void showUI()
    {
        var objects = GameObject.FindGameObjectsWithTag("Pregame");
        foreach (GameObject obj in objects)
        {
            CanvasRenderer rend = obj.GetComponent<CanvasRenderer>();
            rend.SetAlpha(1);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    private IEnumerator StartGame()
    {
        while (gameTime > 0)
        {
            gameTime--;
            uiManager.UpdateTime(gameTime);
            yield return new WaitForSeconds(1);
        }
    }

    private void StartSoundtrack()
    {
        soundtrack.Play();
    }

    private void StopSoundtrack()
    {
        soundtrack.Stop();
    }
}
