using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager getInstance() {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<GameManager>();
        }
        
        return instance;
    }
    
    // for pre-level splash scene
    public int levelID = -1; // number of the currently selected level
    public string levelName; // name of the currently selected level
    public string sceneName; // name of the currently selected scene
    
    
    public int score = 0;
    public float time = 0;
    
    public List<Item> items = new List<Item>();

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
