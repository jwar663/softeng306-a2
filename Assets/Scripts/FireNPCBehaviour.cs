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

    public override void interact(Player player)
    {
        if (player.fireTreesLeft == 0)
        {
            List<string> sentences = new List<string>();
            sentences.Add("You shall pass");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(sentences);
            Destroy(this.gameObject);
        }
        else
        {
            List<string> sentences = new List<string>();
            sentences.Add("You shall not pass");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(sentences);
        }
    }
}