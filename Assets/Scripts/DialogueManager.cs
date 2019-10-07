﻿using System.Collections;
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

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        ignoringFirstPress = true;
        sentences = new Queue<string>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space)) {
            if (ignoringFirstPress) {
                ignoringFirstPress = false;
            } else {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        player.setCanMove(false);
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

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
        ignoringFirstPress = true;
        player.setCanMove(true);
        animator.SetBool("IsOpen", false);
    }

}
