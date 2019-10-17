using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager getInstance() {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<GameManager>();
        }
        
        return instance;
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
        
        items.Add((new Item()).actualConstructor("Water Bucket"));
        items.Add((new Item()).actualConstructor("Water Gun"));
        items.Add((new Item()).actualConstructor("Fire Hose"));
        items.Add((new Item()).actualConstructor("???"));
        
        items[0].unlocked = true;
        items[2].unlocked = true;
    }
    
    // for pre-level splash scene
    public int levelID = -1; // number of the currently selected level
    public string levelName; // name of the currently selected level
    public string sceneName; // name of the currently selected scene
    
    
    public int score = 0;
    public float time = 0;
    
    public List<Item> items = new List<Item>();
}
