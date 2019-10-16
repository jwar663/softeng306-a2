using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float projectileSpeed;
    public bool movesHorizontal;
    public GameObject baseFireball;
    public GameObject player;
    
    private bool alive;
    private int direction;
    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        
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
        
        GameObject fireball = GameObject.Instantiate(baseFireball, projectilePosition, projectileRotation) as GameObject;
        
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        fireballScript.move(force);
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (gameObject != null) {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (player != null) {
                player.reduceHP(10);
                player.setOnFire(true);
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other) {
        if (gameObject != null) {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (player != null) {
                player.setOnFire(false);
            }
        }
    }
    
    public void switchDirection() {
        direction *= -1;
    }
}
