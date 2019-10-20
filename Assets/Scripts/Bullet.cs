using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isBase;
    private Rigidbody2D rigidBody;
    private bool hit;
    
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void move(Vector2 force) {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(force);
    }
    
    // destroy if it leaves vision
    void OnBecameInvisible() {
        if (!isBase) {
            Destroy(gameObject);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet" || other.gameObject.tag == "Waterball" || other.gameObject.tag == "Fireball" || other.gameObject.tag == "FireEnemy" || isBase) {
            return;
        }
        
        // projectiles hit twice for some reason
        if (hit) {
            return;
        }
        hit = true;
        
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && other.gameObject.tag == "HumanEnemy") {
            enemy.kill();
        }
        
        Destroy(gameObject);
    }
}
