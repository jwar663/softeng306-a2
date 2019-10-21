using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTornado : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            Vector2 force = new Vector2();
            force.x = transform.position.x - collision.transform.position.x;
            force.y = transform.position.y - collision.transform.position.y;
            collision.attachedRigidbody.AddForce(force);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.attachedRigidbody.velocity = Vector3.zero;
            collision.attachedRigidbody.angularVelocity = 0f;
        }
    }
}
