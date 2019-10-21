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
    public SortedDictionary<int, string> highScores = new SortedDictionary<int, string>() { { 0, "Jogn" },{ 2, "icar" } }; //Dictionary of scores sorted from lowest to highest score.

    public void setScore(int score, string name)
    {
        highScores.Add(score, name);
    }

    // for the level select scene
    public int levelsCompleted = 0;
    public bool watchedCutscene = false; // this is necessary so the player doesn't have to watch the same cutscene twice if they quit the level
    
    public List<Item> items = new List<Item>();
    
    public GameObject baseWaterball;
    public float waterballSpeed;
    public float waterballCooldown;
    
    public GameObject baseBullet;
    public float bulletSpeed;
    public float bulletCooldown;
    
    public GameObject baseEnemyBullet;
    public float enemyBulletSpeed;
    
    public bool cheatUnlockPortals = false;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(baseWaterball);
        DontDestroyOnLoad(baseBullet);
        DontDestroyOnLoad(baseEnemyBullet);
        instance = this;
    }
}
