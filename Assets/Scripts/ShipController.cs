using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{

    public float speed;
    public float suctionSpeed;
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

    private int selectedItemIndex;
    private Item selectedItem;

    private float waterballCooldown;

    enum Direction
    {
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
        selectedItemIndex = 0;
        //selectedItem = GameManager.getInstance().items[selectedItemIndex];

        controller = FindObjectOfType<ForestGameController>();

        if (controller)
        {
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

        if (onFire && Random.Range(0.0f, 1.0f) < 0.25f)
        {
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            effectiveSpeed *= 2;
        }

        myRigidbody.MovePosition(transform.position + change * effectiveSpeed * Time.deltaTime);
    }

    void resetActingAnimationState()
    {
        animator.SetBool("acting", false);
    }

    void updateKeyboard()
    {
        // update the direction the player is facing
        // this doesn't actually change how they're rendered, but we need this information later
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            facingDirection = Direction.UP;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            facingDirection = Direction.DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            facingDirection = Direction.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingDirection = Direction.RIGHT;
        }

        // if 'x' pressed
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
        {

        }


    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space))
        {
            if (collision.gameObject.tag.Equals("Oil Field"))
            {
                collision.gameObject.transform.localScale -= new Vector3(suctionSpeed, suctionSpeed, suctionSpeed) * Time.deltaTime;
                Debug.Log("On Oil field");
                if (collision.gameObject.transform.localScale.x < 0.01f)
                {
                    Destroy(collision.gameObject);
                }
            }
            if (collision.gameObject.tag.Equals("Duck"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Water Tornado"))
        {
            reduceHP(10);
        }
    }

    private void changeItem(int offset)
    {
        selectedItemIndex = getItemIndex(offset);
        selectedItem = GameManager.getInstance().items[selectedItemIndex];

        updateItemView();
    }

    private int getItemIndex(int offset)
    {
        int totalItems = GameManager.getInstance().items.Count;

        int index = selectedItemIndex + offset;
        index = ((index % totalItems) + totalItems) % totalItems;
        while (!GameManager.getInstance().items[index].unlocked)
        {
            index += offset;
            index = ((index % totalItems) + totalItems) % totalItems;
        }

        return index;
    }

    private void updateItemView()
    {
        Item previous = GameManager.getInstance().items[getItemIndex(-1)];
        Item next = GameManager.getInstance().items[getItemIndex(1)];

        FindObjectOfType<ItemManager>().setItems(previous, selectedItem, next);
    }

    public void reduceHP(int x)
    {
        hp -= x;

        if (hp <= 0)
        {
            hp = 0;
            alive = false;
            SceneManager.LoadScene("GameOverScene");
        }

        if (x >= 10)
        {
            FindObjectOfType<AudioManager>().Play("Pain");
        }

        Debug.Log("HP: " + hp);
        FindObjectOfType<ProgressBar>().GetCurrentProgress(getHP());
    }

    public int getHP()
    {
        return hp;
    }

    public void setOnFire(bool onFire)
    {
        this.onFire = onFire;
    }

    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void shootWaterball()
    {
        if (Time.time - waterballCooldown < GameManager.getInstance().waterballCooldown)
        {
            return;
        }
        waterballCooldown = Time.time;

        Vector2 force;
        float projectileSpeed = GameManager.getInstance().waterballSpeed;

        switch (facingDirection)
        {
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
}
