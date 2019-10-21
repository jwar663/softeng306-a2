using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParisGameController: MonoBehaviour, LevelController {
    private List<NPC> npcs;
    
    public int enemiesKilled = 0;
    public int targetEnemiesKilled = 1;
    
    void Start() {
        npcs = new List<NPC>(FindObjectsOfType<NPC>());
    }

    void Update() {
        
    }
    
    public List<NPC> getNPCs() {
        return npcs;
    }
}
