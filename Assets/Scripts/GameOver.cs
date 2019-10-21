using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void returnToMenu() {
      
        SceneManager.LoadScene("MenuScene");
    }

    public void retry()
    {
       
        SceneManager.LoadScene(GameManager.getInstance().sceneName);
    }

    public void playClick() {
        FindObjectOfType<AudioManager>().Play("Confirm");
    }
}
