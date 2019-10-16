using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string levelName;
    public string sceneName;
    public int levelID;

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.getInstance().levelName = levelName;
        GameManager.getInstance().sceneName = sceneName;
        GameManager.getInstance().levelID = levelID;
        SceneManager.LoadScene("LevelStartTransitionScene");
    }
}
