using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> dialogue = new List<string>();
        dialogue.Add("Hello worlds");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(dialogue);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
