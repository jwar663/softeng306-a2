using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

NPCs should have an NPC.cs script and another script extending the abstract NPCBehaviour.cs. There may be a better way to do this, but then again, this language was designed for and by failed lobotomy experiments.

*/
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void talkTo(Player player) {
        NPCBehaviour behaviour = GetComponent<NPCBehaviour>();
        
        if (behaviour != null) {
            behaviour.interact(player);
        }
    }
}
