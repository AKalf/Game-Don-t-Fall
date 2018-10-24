using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinking : MonoBehaviour {
    /*
     * Makes gameobjects sink while player is stepping on them
     * They sink faster from the side that player is on (comparing p.transform with this.transform)
     * 
     */ 
    [SerializeField]
    float sinkingSpeed;
    bool playerIsOn = false;
    Vector3 startingPosition;
    Vector3 startingRot;
    List<Transform> playersOnObject = new List<Transform>();

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
        startingRot = transform.rotation.eulerAngles;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (playerIsOn)
        {
            Sink();
        }
       
        else {
            ResetPosition();
        }
	}
    private void Sink() {
        foreach (Transform player in playersOnObject)
        {
            int zRotated;
            if (player.position.z > transform.position.z)
            {
                zRotated = 1;
            }
            else
            {
                zRotated = -1;
            }
            // rotate towards the side of the player
            transform.Rotate(Vector3.forward, zRotated * Time.deltaTime);
        }
        // decrease Y position
        transform.Translate(-Vector3.forward * sinkingSpeed * Time.deltaTime);
    }
    private void ResetPosition() {
        // reset rotation until starting rotation
        if ((int)transform.rotation.eulerAngles.z != (int)startingRot.z)
        {
            transform.Rotate((startingRot - transform.rotation.eulerAngles));
        }
        // reset Y position until starting Y position
        if (transform.position.y != startingPosition.y)
        {
            transform.position += (Vector3.up * (startingPosition.y - transform.position.y) * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.StartsWith("Player")) {
            playerIsOn = true;
            playersOnObject.Add(collision.transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.StartsWith("Player"))
        {
            playersOnObject.Remove(collision.transform);
          
            if (playersOnObject.Count == 0) {
                playerIsOn = false;
            }
        }
    }
   
}
