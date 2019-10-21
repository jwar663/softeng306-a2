using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        if (other.gameObject.tag == "BodyguardEnemy" || other.gameObject.tag == "Bullet" || other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Waterball" || isBase) {
            return;
        }
        
        // projectiles hit twice for some reason
        if (hit) {
            return;
        }
        hit = true;
        
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null && !player.isShielded()) {
            player.reduceHP(20);
            FindObjectOfType<AudioManager>().Play("Pain");
        }
        
        Destroy(gameObject);
    }
}
