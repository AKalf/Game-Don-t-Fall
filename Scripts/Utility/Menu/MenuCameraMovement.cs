using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour {

    [SerializeField]
    float speed = 1;
    [SerializeField]
    Transform postionOne;
    [SerializeField]
    Transform positionTwo;

    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float x = -Input.GetAxis("Mouse X");

        if (postionOne.position.z > transform.position.z && x > 0)
        {
            transform.position += new Vector3(0, 0, x * Time.deltaTime * speed);
        }
        else if (positionTwo.position.z < transform.position.z && x < 0) {
            transform.position += new Vector3(0, 0, x * Time.deltaTime * speed);
        }
    }
}
