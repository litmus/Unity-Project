﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld Scene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
