using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour {


    /*
     * Moves the gameobject up and down and rotates it around the X axis
     * Moves between a min and max offset from its starting Y position
     * Positive rotation on X axis from a given time, then rotates back to starting rotation
     * The purpose of the script is to give a "floating on liquid" effect
     */
    bool goinUp = true;
    bool resettingRotation = false;

    [SerializeField]
    float maxY = 0.05f;
    [SerializeField]
    float minY = -0.05f;
    [SerializeField]
    float speed = 0.1f;

    [SerializeField]
    float maxTimeRotating = 2f;
    [SerializeField]
    float rotatingSpeed = 0.05f;
    Vector3 initialPos = new Vector3();

    float timeRotating = 0; // used to count how much has the gameobject rotated from starting rotation

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        MoveVertiacaly();
        if (resettingRotation) {
            RotateToStartingRotation();
        }
        else  {
            RotateRight();
        }
       
        
    }
    private void RotateRight() {
        transform.Rotate(Vector3.right, rotatingSpeed * Time.deltaTime);
        timeRotating += Time.deltaTime;
        if (timeRotating >= maxTimeRotating)
        {
            resettingRotation = true;
            timeRotating = 0;
        }
    }
    private void RotateToStartingRotation() {
        transform.Rotate(-Vector3.right, rotatingSpeed * Time.deltaTime);
        timeRotating += Time.deltaTime;
        if (timeRotating >= maxTimeRotating)
        {
            resettingRotation = false;
            timeRotating = 0;
        }
    }
    private void MoveVertiacaly() {
        if (goinUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= initialPos.y + maxY)
            {
                goinUp = false;
            }
        }
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if (transform.position.y <= initialPos.y + minY)
            {
                goinUp = true;
            }

        }
    }
     
}
