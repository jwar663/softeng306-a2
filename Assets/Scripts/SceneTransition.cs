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
        if (GameManager.getInstance().cheatUnlockPortals || levelID <= GameManager.getInstance().levelsCompleted + 1) {
            GameManager.getInstance().levelName = levelName;
            GameManager.getInstance().sceneName = sceneName;
            GameManager.getInstance().levelID = levelID;
            SceneManager.LoadScene("LevelStartTransitionScene");
        } else {
            FindObjectOfType<ToastMessage>().show("That portal is not open yet.");
        }
    }
}
