using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardEnemy: MonoBehaviour
{
    public float speed;
    public float projectileSpeed;
    public bool movesHorizontal;
    public GameObject player;
    
    private bool alive;
    private int direction;
    private Rigidbody2D rigidBody;
    private Animator animator;
    
    private int hp = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        switchDirection();
        InvokeRepeating("switchDirection", 4.0f, 4.0f);
        InvokeRepeating("shoot", 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 change = Vector3.zero;
        
        if (movesHorizontal) {
            change.x = direction * speed * Time.deltaTime;
        } else {
            change.y = direction * speed * Time.deltaTime;
        }
        
        rigidBody.MovePosition(transform.position + change);
    }
    
    public void shoot() {
        float angle = Random.Range(0.0f, 2 * Mathf.PI);
        
        //float gradient = (player.transform.position.y - transform.position.y) / (player.transform.position.x - transform.position.x);
        //float gradient = (player.transform.position.x - transform.position.x) / (player.transform.position.y - transform.position.y);
        //float angle = Mathf.Tan(gradient);
        //angle += Random.Range(-0.1f, 0.1f);
        
        Vector2 force = new Vector2(Mathf.Sin(angle) * projectileSpeed, Mathf.Cos(angle) * projectileSpeed);
        //float angleDegrees = 180 * angle / Mathf.PI;
        float angleDegrees = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        
        Vector3 projectilePosition = transform.position;
        Quaternion projectileRotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);
        
        GameObject bullet = GameObject.Instantiate(GameManager.getInstance().baseEnemyBullet, projectilePosition, projectileRotation) as GameObject;
        
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.isBase = false;
        bulletScript.move(force);
    }
    
    public void switchDirection() {
        direction *= -1;
        if (movesHorizontal) {
            animator.SetFloat("x", direction);
            animator.SetFloat("y", 0);
        } else {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", direction);
        }
    }
    
    public void kill() {
        hp -= 1;
        
        if (hp == 0) {
            Destroy(gameObject);
        }
    }
}
