using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistNPCBehaviour: NPCBehaviour {
    public int interactionID;
    
    private Vector3 truePosition;
    private bool done = false;
    
    void Start() {
        
    }

    void Update() {
        
    }
    
    public override void interact(NPC npc) {
        if (done) {
            return;
        }
        done = true;
        
        List<string> sentences = new List<string>();
        switch (interactionID) {
        case 0:
            sentences.Add("Good to see you.");
            sentences.Add("Meet me at the portal, it's the first turn on your left down the hall.");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, false);
            break;
        case 1:
            sentences.Add("We went over this already, but I'll brief you again to make sure you get this right.");
            sentences.Add("We've sent an operative through already to help you out.");
            sentences.Add("etc.");
            sentences.Add("Use this bucket to put out the fires.");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, false);
            break;
        case 2:
            sentences.Add("Well done on extinguishing those fires.");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, false);
            break;
        default:
            break;
        }
    }
    
    public override void dialogueCompleted() {
        switch (interactionID) {
        case 0:
            Destroy(gameObject);
            break;
        case 1:
            GameManager.getInstance().items[0].unlocked = true;
            FindObjectOfType<ToastMessage>().show("Received item: Bucket");
            Destroy(gameObject);
            break;
        case 2:
            Destroy(gameObject);
            break;
        default:
            break;
        }
    }
    
    public void hideOffStage() {
        truePosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(-999, -999, 0);
    }
    
    public void showOnStage() {
        gameObject.transform.position = truePosition;
    }
}
