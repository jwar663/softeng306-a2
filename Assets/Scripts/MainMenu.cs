using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        Game.levelName = "Forest"; // change to level select
        Game.sceneName = "ForestScene"; // change to level select
        Game.levelID = 1; // remove
        SceneManager.LoadScene("LevelStartTransitionScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
        
    }
}
