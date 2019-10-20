using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilNPCBehaviour: NPCBehaviour
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
        
        if (item && item.name == "Hose") {
            Destroy(gameObject);
            
            // TODO: update score and count in ocean level controller
        } else {
            FindObjectOfType<ToastMessage>().show("That won't help you clean up the oil.");
        }
    }
    
    public override void dialogueCompleted() {
        
    }
}
