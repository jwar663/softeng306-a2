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
    
    private Direction facingDirection = Direction.DOWN;
    
    public List<FireTree> fireTrees;

    enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    
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
        updateKeyboard();

        
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
    
    void resetActingAnimationState() {
        animator.SetBool("acting", false);
    }
    
    void updateKeyboard() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            facingDirection = Direction.UP;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            facingDirection = Direction.DOWN;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            facingDirection = Direction.LEFT;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            facingDirection = Direction.RIGHT;
        }
        
        if (Input.GetKeyDown(KeyCode.X)) {
            Vector2 offset;
            switch (facingDirection) {
            case Direction.UP:
                offset = new Vector2(0f, 1f);
                break;
            case Direction.DOWN:
                offset = new Vector2(0f, -1f);
                break;
            case Direction.LEFT:
                offset = new Vector2(-1f, 0f);
                break;
            case Direction.RIGHT:
                offset = new Vector2(1f, 0f);
                break;
            default:
                offset = new Vector2(0f, 0f);
                break;
            }
            
            Vector2 targetPosition = myRigidbody.position + offset;
            
            foreach (FireTree fireTree in fireTrees) {
                Vector2 fireTreePosition = new Vector2(fireTree.gameObject.transform.position.x, fireTree.gameObject.transform.position.y);
                if (fireTreePosition.x - 0.5f < targetPosition.x && fireTreePosition.x + 0.5f > targetPosition.x) {
                    if (fireTreePosition.y - 0.5f < targetPosition.y && fireTreePosition.y + 0.5f > targetPosition.y) {
                        if (fireTree.isOnFire()) {
                            animator.SetBool("acting", true);
                            fireTree.putOut();
                            Invoke("resetActingAnimationState", 0.5f);
                        }
                    }
                }
            }
        }
    }
}
