using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool movesHorizontal;
    
    private bool alive;
    private int direction;
    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("switchDirection", 4.0f, 4.0f);
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
    
    public void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player != null) {
            player.reduceHP(10);
            player.setOnFire(true);
        }
    }
    
    public void OnTriggerExit2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player != null) {
            player.setOnFire(false);
        }
    }
    
    public void switchDirection() {
        direction *= -1;
    }
}
