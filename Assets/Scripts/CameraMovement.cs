using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    //how quickly the camera moves towards the target
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame, FixedUpdate happens whenever the physics system is set to tick
    // a late update comes last, character moves first then camera follows
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);


            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);


        }
    }
}
