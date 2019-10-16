using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNPCBehaviour : NPCBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void interact(NPC npc)
    {
        ForestGameController controller = FindObjectOfType<ForestGameController>();
        if (controller.fireTreesExtinguished >= controller.fireTreesTarget)
        {
            List<string> sentences = new List<string>();
            sentences.Add("You shall pass");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences);
        }
        else
        {
            List<string> sentences = new List<string>();
            sentences.Add("You shall not pass");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, sentences);
        }
    }
    
    public override void dialogueCompleted() {
        ForestGameController controller = FindObjectOfType<ForestGameController>();
        if (controller.fireTreesExtinguished >= controller.fireTreesTarget) {
            Destroy(this.gameObject);
        }
    }
}