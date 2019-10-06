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
    
    public override void interact(PlayerMovement player) {
        if (player.fireTreesLeft == 0) {
            Debug.Log("you shall pass");
            Destroy(this.gameObject);
        } else {
            Debug.Log("you shall not pass");
        }
    }
}
