﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{
    bool notRun = true;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (notRun)
        {
            List<string> dialogue = new List<string>();
            dialogue.Add("Look at the world.....");
            dialogue.Add("....Look at what climate change has done.");
            dialogue.Add("...it has destroyed the world irreversably.");
            dialogue.Add("But there is still hope.");
            dialogue.Add("You must travel back in time to climate disasters and stop them from ever happening");
            dialogue.Add("You are humanities last hope!");
            dialogue.Add("Good luck!");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(dialogue);
            notRun = false;
        }
    }
}
