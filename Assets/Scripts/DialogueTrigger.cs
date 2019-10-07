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
    
    public void TriggerDialogue(NPC npc, List<string> sentences)
    {
        dialogue.npc = npc;
        dialogue.setSentences(sentences);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
