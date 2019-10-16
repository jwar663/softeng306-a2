using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    private int hp;
    private bool onFire;
    private bool alive;
    private bool canMove;
    private ForestGameController controller;
    
    private Rigidbody2D myRigidbody;
    // how much the player's position should change
    private Vector3 change;
    private Animator animator;
    
    // current direction the player is facing
    private Direction facingDirection = Direction.DOWN;
    
    // index of the currently selected item
    private int selectedItem;
    
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
        hp = 100;
        onFire = false;
        alive = true;
        canMove = true;
        
        controller = FindObjectOfType<ForestGameController>();
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
        
        if (onFire && Random.Range(0.0f, 1.0f) < 0.25f) {
            reduceHP(1);
        }
    }

    void updateAnimationAndMove()
    {
        if (change != Vector3.zero && canMove && alive)
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
        float effectiveSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            effectiveSpeed *= 2;
        }
        
        myRigidbody.MovePosition( transform.position + change * effectiveSpeed * Time.deltaTime);
    }
    
    void resetActingAnimationState() {
        animator.SetBool("acting", false);
    }
    
    void updateKeyboard() {
        // update the direction the player is facing
        // this doesn't actually change how they're rendered, but we need this information later
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            facingDirection = Direction.UP;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            facingDirection = Direction.DOWN;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            facingDirection = Direction.LEFT;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            facingDirection = Direction.RIGHT;
        }
        
        // if 'x' pressed
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space)) {
            // calculate what square the player is facing
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
            
            // 1/2 interaction zone
            float delta = 0.75f;
            
            // if the player is facing a firetree that is on fire, extinguish it
            foreach (FireTree fireTree in controller.fireTrees) {
                Vector2 fireTreePosition = new Vector2(fireTree.gameObject.transform.position.x, fireTree.gameObject.transform.position.y);
                if (fireTreePosition.x - delta < targetPosition.x && fireTreePosition.x + delta > targetPosition.x) {
                    if (fireTreePosition.y - delta < targetPosition.y && fireTreePosition.y + delta > targetPosition.y) {
                        if (fireTree.isOnFire()) {
                            animator.SetBool("acting", true);
                            fireTree.putOut();
                            controller.fireTreesExtinguished++;
                            FindObjectOfType<AudioManager>().Play("PutOut");
                            Invoke("resetActingAnimationState", 0.5f);
                            controller.score += 500;
                        }
                    }
                }
            }
            
            // if the player is facing an npc, interact with it
            foreach (NPC npc in controller.npcs) {
                if (npc == null) {
                    continue;
                }
                
                Vector2 npcPosition = new Vector2(npc.gameObject.transform.position.x, npc.gameObject.transform.position.y);
                if (npcPosition.x - delta < targetPosition.x && npcPosition.x + delta > targetPosition.x) {
                    if (npcPosition.y - delta < targetPosition.y && npcPosition.y + delta > targetPosition.y) {
                        if (canMove) {
                            npc.talkTo();
                        }
                    }
                }
            }
        }
    }
    
    public void reduceHP(int x) {
        hp -= x;
        
        if (hp <= 0) {
            hp = 0;
            alive = false;
            SceneManager.LoadScene("GameOverScene");
        }
        
        if (x >= 10) {
            FindObjectOfType<AudioManager>().Play("Pain");
        }
        
        Debug.Log("HP: " + hp);
        FindObjectOfType<ProgressBar>().GetCurrentProgress(getHP());
    }

    public int getHP() {
        return hp;
    }
    
    public void setOnFire(bool onFire) {
        this.onFire = onFire;
    }
    
    public void setCanMove(bool canMove) {
        this.canMove = canMove;
    }
}
