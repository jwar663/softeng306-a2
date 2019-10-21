using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    private int hp;
    private bool onFire;
    private bool alive;
    private bool canMove;
    private bool shielded;
    private LevelController controller;
    
    private Rigidbody2D myRigidbody;
    // how much the player's position should change
    private Vector3 change;
    private Animator animator;
    
    // current direction the player is facing
    private Direction facingDirection = Direction.DOWN;
    
    private int selectedItemIndex;
    private Item selectedItem;
    
    private float waterballCooldown;
    private float bulletCooldown;
    
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
        shielded = false;
        selectedItemIndex = 0;
        selectedItem = GameManager.getInstance().items[selectedItemIndex];
        
        // find controller
        IEnumerator enumerator = FindObjectsOfType<MonoBehaviour>().OfType<LevelController>().GetEnumerator();
        enumerator.MoveNext();
        controller = enumerator.Current as LevelController;
        
        if (controller != null) {
            updateItemView();
        }
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
            
            bool interactedWithNPC = false;
            
            if (controller != null) {
                // if the player is facing an npc, interact with it
                foreach (NPC npc in controller.getNPCs()) {
                    if (npc == null) {
                        continue;
                    }
                    
                    Vector2 npcPosition = new Vector2(npc.gameObject.transform.position.x, npc.gameObject.transform.position.y);
                    if (npcPosition.x - delta < targetPosition.x && npcPosition.x + delta > targetPosition.x) {
                        if (npcPosition.y - delta < targetPosition.y && npcPosition.y + delta > targetPosition.y) {
                            if (canMove) {
                                interactedWithNPC = true;
                                npc.talkTo();
                                break;
                            }
                        }
                    }
                }
                
                ForestGameController forest = controller as ForestGameController;
                if (forest) {
                    if (!interactedWithNPC) {
                        if (selectedItem.name == "Water Bucket") {
                            if (selectedItem.useOtherSprite) { // bucket is empty
                                GameObject fountain = GameObject.FindWithTag("Fountain");
                                float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(fountain.transform.position.x - transform.position.x), 2.0f) + Mathf.Pow(Mathf.Abs(fountain.transform.position.y - transform.position.y), 2.0f));
                                if (distance <= 2.0f) {
                                    selectedItem.useOtherSprite = false;
                                    updateItemView();
                                }
                            }
                        }
                        
                        // if the player is facing a firetree that is on fire, extinguish it
                        foreach (FireTree fireTree in forest.fireTrees) {
                            Vector2 fireTreePosition = new Vector2(fireTree.gameObject.transform.position.x, fireTree.gameObject.transform.position.y);
                            if (fireTreePosition.x - delta < targetPosition.x && fireTreePosition.x + delta > targetPosition.x) {
                                if (fireTreePosition.y - delta < targetPosition.y && fireTreePosition.y + delta > targetPosition.y) {
                                    if (fireTree.isOnFire()) {
                                        if (selectedItem.name == "Water Bucket" ){
                                            if (selectedItem.useOtherSprite) {
                                                FindObjectOfType<ToastMessage>().show("Your bucket is empty!");
                                            } else {
                                                animator.SetBool("acting", true);
                                                fireTree.putOut();
                                                forest.fireTreesExtinguished++;
                                                FindObjectOfType<AudioManager>().Play("PutOut");
                                                Invoke("resetActingAnimationState", 0.5f);
                                                forest.score -= 1;
                                                selectedItem.useOtherSprite = true;
                                                updateItemView();
                                            }
                                        } else {
                                            FindObjectOfType<ToastMessage>().show("That won't put out the fire.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        if (controller != null && selectedItem != null) {
            if (Input.GetKey(KeyCode.X)) {
                switch (selectedItem.name) {
                case "Water Gun":
                    shootWaterball();
                    break;
                case "Gun":
                    shootBullet();
                    break;
                default:
                    break;
                }
            }
        }
        
        if (controller != null && selectedItem != null) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                changeItem(-1);
            } else if (Input.GetKeyDown(KeyCode.E)) {
                changeItem(1);
            }
        }
        
        shielded = controller != null && selectedItem != null && selectedItem.name == "Shield" && (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space));
        animator.SetBool("shielded", shielded);
    }
    
    public bool isShielded() {
        return shielded;
    }
    
    private void changeItem(int offset) {
        selectedItemIndex = getItemIndex(offset);
        
        if (selectedItemIndex != -1) {
            selectedItem = GameManager.getInstance().items[selectedItemIndex];
            updateItemView();
        }
    }
    
    private int getItemIndex(int offset) {
        // prevent infinite loop
        bool noneUnlocked = true;
        foreach (Item item in GameManager.getInstance().items) {
            if (item.unlocked) {
                noneUnlocked = false;
            }
        }
        if (noneUnlocked) {
            return -1;
        }
        
        int totalItems = GameManager.getInstance().items.Count;
        
        int index = selectedItemIndex + offset;
        index = ((index % totalItems) + totalItems) % totalItems;
        while (!GameManager.getInstance().items[index].unlocked) {
            index += offset;
            index = ((index % totalItems) + totalItems) % totalItems;
        }
        
        return index;
    }
    
    public void updateItemView() {
        if (selectedItemIndex == -1) {
            return;
        }
        
        Item previous = GameManager.getInstance().items[getItemIndex(-1)];
        Item next = GameManager.getInstance().items[getItemIndex(1)];
        
        if (FindObjectOfType<ItemManager>() != null) {
            FindObjectOfType<ItemManager>().setItems(previous, selectedItem, next);
        }
    }
    
    public Item getSelectedItem() {
        return selectedItem;
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
    
    public void shootWaterball() {
        if (Time.time - waterballCooldown < GameManager.getInstance().waterballCooldown) {
            return;
        }
        waterballCooldown = Time.time;
        
        Vector2 force;
        float projectileSpeed = GameManager.getInstance().waterballSpeed;

        switch (facingDirection) {
        case Direction.UP:
            force = new Vector2(0f, projectileSpeed);
            break;
        case Direction.DOWN:
            force = new Vector2(0f, -projectileSpeed);
            break;
        case Direction.LEFT:
            force = new Vector2(-projectileSpeed, 0f);
            break;
        case Direction.RIGHT:
            force = new Vector2(projectileSpeed, 0f);
            break;
        default:
            force = new Vector2(0f, projectileSpeed);
            break;
        }
        float angleDegrees = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        
        Vector3 projectilePosition = transform.position;
        Quaternion projectileRotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);
        
        GameObject waterball = GameObject.Instantiate(GameManager.getInstance().baseWaterball, projectilePosition, projectileRotation) as GameObject;
        
        Waterball waterballScript = waterball.GetComponent<Waterball>();
        waterballScript.isBase = false;
        waterballScript.move(force);
    }
    
    public void shootBullet() {
        if (Time.time - bulletCooldown < GameManager.getInstance().bulletCooldown) {
            return;
        }
        bulletCooldown = Time.time;
        
        Vector2 force;
        float projectileSpeed = GameManager.getInstance().bulletSpeed;

        switch (facingDirection) {
        case Direction.UP:
            force = new Vector2(0f, projectileSpeed);
            break;
        case Direction.DOWN:
            force = new Vector2(0f, -projectileSpeed);
            break;
        case Direction.LEFT:
            force = new Vector2(-projectileSpeed, 0f);
            break;
        case Direction.RIGHT:
            force = new Vector2(projectileSpeed, 0f);
            break;
        default:
            force = new Vector2(0f, projectileSpeed);
            break;
        }
        float angleDegrees = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        
        Vector3 projectilePosition = transform.position;
        Quaternion projectileRotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);
        
        GameObject bullet = GameObject.Instantiate(GameManager.getInstance().baseBullet, projectilePosition, projectileRotation) as GameObject;
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.isBase = false;
        bulletScript.move(force);
    }
}
