﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        FindObjectOfType<AudioManager>().Play("Confirm");
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        //FindObjectOfType<AudioManager>().Play("Confirm");
        Debug.Log("Quit game");
        Application.Quit();
        
    }

    public void playClick()
    {
        FindObjectOfType<AudioManager>().Play("Confirm");
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            GameManager.getInstance().cheatUnlockPortals = true;
        }
        
        if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.getInstance().cheatExtraDamage = true;
        }
    }
}
