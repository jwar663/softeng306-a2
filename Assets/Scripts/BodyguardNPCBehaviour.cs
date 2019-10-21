using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardNPCBehaviour: NPCBehaviour {
    
    void Start() {
        
    }

    void Update() {
        
    }
    
    public override void interact(NPC npc) {
        List<string> sentences = new List<string>();
        ParisGameController paris = FindObjectOfType<ParisGameController>();
        if (paris.enemiesKilled >= paris.targetEnemiesKilled) {
            sentences.Add("Such terrifying power!");
            sentences.Add("Please, spare my life in exchange for not blocking this path!");
        } else {
            sentences.Add("Hey, you!");
            sentences.Add("Bodies belong in the morgue, not the trash!");
        }
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, true);
    }
    
    public override void dialogueCompleted() {
        ParisGameController paris = FindObjectOfType<ParisGameController>();
        if (paris.enemiesKilled >= paris.targetEnemiesKilled) {
            Destroy(gameObject);
        }
    }
}
