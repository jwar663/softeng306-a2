using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNPCBehaviour: NPCBehaviour
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
        Item item = FindObjectOfType<Player>().getSelectedItem();
        
        if (item && item.name == "Net") {
            Destroy(gameObject);
            
            // TODO: update score and count in ocean level controller
        } else {
            FindObjectOfType<ToastMessage>().show("That won't help you save the fish.");
        }
    }
    
    public override void dialogueCompleted() {
        
    }
}
