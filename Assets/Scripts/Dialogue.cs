using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool isOnInteraction;
    
    //[TextArea(1,100)]
    public List<string> sentences;
    public NPC npc;

    public void setSentences(List<string> AllSentences)
    {
        sentences = AllSentences;
    }
}
