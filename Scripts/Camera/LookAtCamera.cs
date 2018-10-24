using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    /*
     * Makes the 3d text of players to look at camera
     */
    [SerializeField]
    GameObject playerCamera = null;
	// Use this for initialization
	void Start () {

        if (playerCamera == null) {
            throw new System.Exception("There is no camera attached to playerText: " + transform.parent.name);
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.forward = transform.position - playerCamera.transform.position;
    }
}
