using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void returnToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void retry()
    {
        SceneManager.LoadScene("ForestScene");
    }
}
