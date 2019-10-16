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
        if (GameManager.getInstance().levelID == -1) {
            titleText.text = GameManager.getInstance().levelName;
        } else {
            titleText.text = "Level " + GameManager.getInstance().levelID + ": " + GameManager.getInstance().levelName;
        }
        
        Debug.Log("Level " + GameManager.getInstance().levelID + ": " + GameManager.getInstance().levelName);
        
        Invoke("startScene", 2.0f);
    }
    
    void startScene() {
        SceneManager.LoadScene(GameManager.getInstance().sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
