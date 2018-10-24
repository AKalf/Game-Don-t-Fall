using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    /*
     * Orbit camera around player on Y axis
     * Get input from mouse or right joystick
     */ 
    GameObject target = null;
    [SerializeField]
    float speed = 5;
    bool boy = true;
    [SerializeField]
    private Vector3 offset = new Vector3(10, 10, 10);

    bool searching = true;
    // Use this for initialization
    private void Awake()
    {
        
    }

    void Start () {
        if (gameObject.name.StartsWith("P1"))
        {
            target = GameObject.FindGameObjectWithTag("PlayerBoy");
            boy = true;
        }
        else
        {
            boy = false;
            StartCoroutine(SearchForGirl());
        }
       

    }
	
	// Update is called once per frame
	void Update () {      
        if (boy)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * speed, Vector3.up) * offset;
            transform.position = target.transform.position + offset;
            transform.LookAt(target.transform.position);
        }
        else if (!searching) {
            if (Input.GetJoystickNames().Length > 0)
            {
                offset = Quaternion.AngleAxis(Input.GetAxis("JoyDown") * Time.deltaTime * speed, Vector3.up) * offset;
            }
            else {
                float x = 0;
                if (Input.GetKey(KeyCode.Keypad0))
                {
                    x = 1;
                }
                else if (Input.GetKey(KeyCode.KeypadPeriod)) {
                    x = -1;
                }
                offset = Quaternion.AngleAxis(x * Time.deltaTime * speed, Vector3.up) * offset;
            }
           
            transform.position = target.transform.position + offset;
            transform.LookAt(target.transform.position);
        }     
    }
    IEnumerator SearchForGirl() {
        yield return new WaitForSeconds(1);
        target = GameObject.FindGameObjectWithTag("PlayerGirl");
        if (target != null) {
            searching = false;
        }
    }
}
