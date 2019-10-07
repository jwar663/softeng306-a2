using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Game.levelName = "Forest";
        Game.sceneName = "ForestScene";
        Game.levelID = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
