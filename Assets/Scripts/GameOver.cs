using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void returnToMenu() {
        //sound doesnt work
        FindObjectOfType<AudioManager>().Play("Confirm");
        SceneManager.LoadScene("MenuScene");
    }

    public void retry()
    {
        FindObjectOfType<AudioManager>().Play("Confirm");
        SceneManager.LoadScene("ForestScene");
    }
}
