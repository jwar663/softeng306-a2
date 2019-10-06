using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTree : MonoBehaviour
{
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void putOut() {
        Debug.Log("put out");
        animator.Play("putOut");
    }
}
