using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Moves the gameobject between two transforms
 * If not set to "move" from the start of the scene, the gameobject will automatically start when touches lava (Bed-event)
 */
public class MovingPlatform : MonoBehaviour {
    [SerializeField]
    bool shouldMove;
    
    [SerializeField]
    public Transform positionOneTransform;
    [SerializeField]
    public Transform positionTwoTransform;
    [SerializeField]
    float speed = 2.5f;
    [SerializeField]

    Vector3 positionOne = Vector3.zero;
    Vector3 positionTwo = Vector3.zero;
    Vector3 currentTarget;
   
    Rigidbody rb;


    // Use this for initialization
    void Start() {
        
        if (positionOneTransform == null || positionTwoTransform == null)
        {
            throw new System.Exception("A target transform has not been assigned to gameobject: " + gameObject.name + "at -Moving platform- component");
        }
        positionOne = positionOneTransform.position;
        positionTwo = positionTwoTransform.position;
        currentTarget = positionTwo;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (shouldMove) {
            // check if target position reached
            if (CheckDifferenceVector3(transform.position, currentTarget, 0.1f))
            {
                // Wait a little and change target
                ChangeTarget();
            }
            // else keep moving towards target
            else {
                // if target is far
                if (Vector3.Distance(transform.position, currentTarget) > 1f)
                {
                    // move with given speed
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed);
                }
                else {
                    // else decrease speed as you approach target
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed * Vector3.Distance(currentTarget, transform.position));
                }
            }
        }
       
    }
    IEnumerator ChangeTarget() {
        yield return new WaitForSeconds(1.5f);
        if (currentTarget == positionTwo)
        {
            currentTarget = positionOne;
        }
        else {
            currentTarget = positionTwo;
        }
    }
   
    bool CheckDifferenceVector3(Vector3 a, Vector3 b, float allowedDifference) {
        if (Vector3.Distance(a, b) <= allowedDifference)
        {
            
            return true;
        }
        else {
            return false;
        }
    }

    public void SetShouldMove(bool b) {
        shouldMove = b;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "floor_0" || collision.gameObject.name == "DeathFloor" && !shouldMove)
        {
            rb.useGravity = false; 
            rb.isKinematic = true; // disable physics
            StartCoroutine(StartMoving()); // give some time to player to get on and start moving
        }
    }

    IEnumerator StartMoving() { 
        yield return new WaitForSeconds(3);
        shouldMove = true;
    }
}
