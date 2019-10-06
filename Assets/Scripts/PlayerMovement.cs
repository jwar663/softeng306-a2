using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D myRigidbody;
    // how much the player's position should change
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // every frame reset how much the player has changed
        change = Vector3.zero;
        //GENERAL NOTE : normalizing = up+right velocities dont get added
        //horizontal and vertical are defined by default in unity
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        //Debug.Log(change);
        updateAnimationAndMove();

        
    }

    void updateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    //method to allow movement, because want to be able to move the character from other places. Not just keyboa rd
    void MoveCharacter()
    {
        myRigidbody.MovePosition( transform.position + change * speed * Time.deltaTime);
    }
}
