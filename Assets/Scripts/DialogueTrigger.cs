using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * DialogueTrigger should be added to npcs or events with dialogue. They can be triggered through the TriggerDialogue function
 * Call the method through 'FindObjectOfType<DialogueTrigger>().TriggerDialogue(sentences);' where sentences is a list of strings. They will play sequentially till all the dialogue has been seen.
 */
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //Call this function to make the dialogue box appear with
    public void TriggerDialogue(List<string> sentences) {
        TriggerDialogue(null, sentences);
    }
    
    public void TriggerDialogue(NPC npc, List<string> sentences) {
        TriggerDialogue(npc, sentences, true);
    }
    
    // isOnInteraction blocks the first x/space press to stop the dialogue from instantly advancing.
    // there are other kinds of dialogue e.g. triggered by scene start
    public void TriggerDialogue(NPC npc, List<string> sentences, bool isOnInteraction)
    {
        dialogue.npc = npc;
        dialogue.name = npc.name;
        dialogue.setSentences(sentences);
        dialogue.isOnInteraction = isOnInteraction;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
