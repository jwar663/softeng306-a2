﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void onExit()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClick()
    {
        FindObjectOfType<AudioManager>().Play("Confirm");
    }
}
