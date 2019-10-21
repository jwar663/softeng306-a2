using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour, LevelController {
    private List<NPC> npcs;
    
    void Start() {
        npcs = new List<NPC>(FindObjectsOfType<NPC>());
        
        foreach (NPC npc in npcs) {
            npc.name = "Dr. Codex";
            npc.GetComponent<ScientistNPCBehaviour>().hideOffStage();
        }
        
        // move the scientists to the appropriate locations depending on the number of levels the user has completed
        if (!GameManager.getInstance().watchedCutscene) {
            switch (GameManager.getInstance().levelsCompleted) {
            case 0:
                getScientist(0).showOnStage();
                getScientist(1).showOnStage();
                break;
            case 1:
                getScientist(2).showOnStage();
                break;
            case 2:
                getScientist(3).showOnStage();
                break;
            case 3:
                // maybe, or just end the game here
                break;
            default:
                break;
            }
        }
    }

    void Update() {
        
    }
    
    public List<NPC> getNPCs() {
        return npcs;
    }
    
    // GetObjectsOfType returns in wrong order
    private ScientistNPCBehaviour getScientist(int interactionID) {
        foreach (NPC npc in npcs) {
            ScientistNPCBehaviour scientist = npc.GetComponent<ScientistNPCBehaviour>();
            if (scientist.interactionID == interactionID) {
                return scientist;
            }
        }
        return null;
    }
}
