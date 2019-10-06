using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTree : MonoBehaviour
{
    public Animator animator;
    private bool onFire;
    
    // Start is called before the first frame update
    void Start()
    {
        onFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void putOut() {
        Debug.Log("putting out fire");
        animator.Play("putOut");
        onFire = false;
    }
    
    public bool isOnFire() {
        return onFire;
    }
}
