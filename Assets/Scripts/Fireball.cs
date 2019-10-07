using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        // dispose of this projectile after 10 seconds
        Invoke("die", 10.0f);
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
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "FireEnemy") {
            return;
        }
        
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            player.reduceHP(10);
        }
        
        Destroy(gameObject);
    }
}
