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
            sentences.Add("We went over this already, but I'll brief you again to make sure we're on the same page.");
            sentences.Add("The charred wasteland once known as the Amazon rainforest has been devoid of life for centuries now.");
            sentences.Add("Our team has identified the ideal point to intervene and prevent the worst of the wildfires from occuring.");
            sentences.Add("We've sent an operative through already to help you out if you get stuck.");
            sentences.Add("Use bucket to put out the fires.");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, false);
            break;
        case 2:
            sentences.Add("Well done on extinguishing those fires.");
            sentences.Add("Next we'll have you visit the site of the Gulf of Mexico oil spill in 2010.");
            sentences.Add("Clean up the oil, save the ducks, and return in one piece.");
            sentences.Add("The portal is to the right this time.");
            sentences.Add("This may be of use to you.");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences, false);
            break;
        case 3:
            sentences.Add("Excellent, the oceans are now crystal clear thanks to your intervention.");
            sentences.Add("For this one you're going to have to get your hands dirty.");
            sentences.Add("We're sending you back to the corrupt hive of diplomats attending the 2016 signing of the Paris Agreement.");
            sentences.Add("I'm sure the important ones will flee before you can reach them, but make sure you send a message nonetheless.");
            sentences.Add("You're gonna need this.");
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
            GameManager.getInstance().items[3].unlocked = true;
            FindObjectOfType<ToastMessage>().show("Received item: Net");
            Destroy(gameObject);
            break;
        case 3:
            GameManager.getInstance().items[5].unlocked = true;
            FindObjectOfType<ToastMessage>().show("Received item: Gun");
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
