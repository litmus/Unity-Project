using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;


    public void UpdateTime(int seconds)
    {
        _timerText.text = "Time Remaining: " + seconds;
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
}
