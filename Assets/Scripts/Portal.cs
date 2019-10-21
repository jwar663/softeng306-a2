using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int levelID;
    
    // Start is called before the first frame update
    void Start()
    { 
        FindObjectOfType<AudioManager>().Play("Portal");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") {
            return;
        }
        
        int levelsCompleted = GameManager.getInstance().levelsCompleted;
        
        if (levelID > levelsCompleted) {
            GameManager.getInstance().levelsCompleted = levelID;
            GameManager.getInstance().watchedCutscene = false;
        }
        
        SceneManager.LoadScene("EndGame");
    }
}
