using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanNPCBehaviour : NPCBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void interact(NPC npc) {
        Debug.Log("human npc");
    }
    
    public override void dialogueCompleted() {
        
    }
}
