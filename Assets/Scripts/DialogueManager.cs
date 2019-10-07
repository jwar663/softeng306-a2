using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Player player;
    
    private bool ignoringFirstPress;
    private bool inDialogue;

    private Queue<string> sentences;
    private NPC npc;
    
    void Start()
    {
        ignoringFirstPress = true;
        inDialogue = false;
        sentences = new Queue<string>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space)) {
            if (ignoringFirstPress) {
                ignoringFirstPress = false;
            } else if (inDialogue) {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        ignoringFirstPress = true;
        if (player != null)
        {
            player.setCanMove(false);
        }
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        
        npc = dialogue.npc;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //If there are no more sentences, then the dialogue box will dissapear.
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);


    }

    void EndDialogue()
    {
        inDialogue = false;
        if (player != null)
        {
            player.setCanMove(true);
        }
        ignoringFirstPress = true;
        player.setCanMove(true);
        animator.SetBool("IsOpen", false);
        
        if (npc != null) {
            npc.dialogueCompleted();
        }
    }

}
