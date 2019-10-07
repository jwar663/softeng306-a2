using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartTransition : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    
    // Start is called before the first frame update
    void Start()
    {
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
