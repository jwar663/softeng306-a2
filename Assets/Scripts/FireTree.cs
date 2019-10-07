using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Represents a burning tree that can be extinguished by player interaction. This should be turned into an NPC (which should be renamed to 'Interactable' or something) at some point.

*/

public class FireTree : MonoBehaviour
{
    public Animator animator;
    private bool onFire;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("count = " + count);
        onFire = true;
        FindObjectOfType<AudioManager>().Play("TreeOnFire");
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void putOut() {
        FindObjectOfType<AudioManager>().Stop("TreeOnFire");
        Debug.Log("putting out fire");
        animator.Play("putOut");
        onFire = false;
    }
    
    public bool isOnFire() {
        return onFire;
    }
}
