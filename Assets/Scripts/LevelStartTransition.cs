using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStartTransition : MonoBehaviour
{
    private Text titleText;
    
    // Start is called before the first frame update
    void Start()
    {
        titleText = GetComponent<Text>();
        
        if (Game.levelID == -1) {
            titleText.text = Game.levelName;
        } else {
            titleText.text = "Level " + Game.levelID + ": " + Game.levelName;
        }
        
        Invoke("startScene", 2.0f);
    }
    
    void startScene() {
        SceneManager.LoadScene(Game.sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
