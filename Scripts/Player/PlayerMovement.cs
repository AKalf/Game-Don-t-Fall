using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static bool GetInput = true;

    [SerializeField]
    float rotationSpeed = 1.8f;
    [SerializeField]
    float maxSpeed = 2;
    [SerializeField]
    float acceleration = 0.05f; // the acceleration of the player while user holds "forward" button
  

    float currentSpeed = 0; // the current speed of the player
    float jumpingCurrentSpeed = 0;
    public bool grounded = true;
    float angleY = 0; // the angle that player is looking 
    MeshRenderer eText; // a reference for the render that shows "E" UI element, when player is near an interactable object
    Animator animator; // a reference to the component "animator" of the player
    PlayerJump playerJumpComp = null;
    Rigidbody rb;
    int playerNumber = -1;
    int rotatingBoolHash = Animator.StringToHash("Rotating");
    int enableMovemnt = 1;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {

        if (tag == "PlayerBoy")
        {
            playerNumber = 1;
        }
        else if (tag == "PlayerGirl")
        {
            playerNumber = 2;
        }
        else
        {
            throw new System.Exception("Gameboject: " + gameObject.name + "is not tagged neither as PlayerBoy or PlayerGirl");
        }
        // boyEtext = transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();
        playerJumpComp = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetInput)
        {
            if (playerNumber == 1)
            {
                float z = 0;
                float x = 0;
                z = Input.GetAxis("Vertical");
                x = Input.GetAxis("Horizontal");
                if (x != 0) // right or left rotation input
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    // if "D" or "A" is pressed, rotate player to that direction
                    angleY += x * rotationSpeed;
                    transform.rotation = Quaternion.Euler(0, angleY, 0);
                    animator.SetBool(rotatingBoolHash, true);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    animator.SetBool(rotatingBoolHash, false);
                }
                if (playerJumpComp.GetGrounded())
                {
                    
                    MoveOnGround(z);
                    animator.SetFloat("speed", currentSpeed); // set the parameter "speed" in the animator controler of "animator" to the same value as "currentSpeed"
                }
                else
                {
                    MoveOnAir(z);
                }
            }
            else
            {
                float x = 0;
                float z = 0;
                x = Input.GetAxis("JoyHorizontal");
                z = Input.GetAxis("JoyVertical");
                if (z < 0)
                {
                    z = 0;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    x = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    x = 1;
                }
                else
                {
                    x = 0;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    z = -1;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    z = 1;
                }
                else
                {
                    z = 0;
                }
                if (x != 0) // right or left rotation input
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    // if "D" or "A" is pressed, rotate player to that direction
                    angleY += x * rotationSpeed;
                    transform.rotation = Quaternion.Euler(0, angleY, 0);
                    animator.SetBool(rotatingBoolHash, true);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    animator.SetBool(rotatingBoolHash, false);
                }
                if (playerJumpComp.GetGrounded())
                {
                    MoveOnGround(z);
                    animator.SetFloat("speed", currentSpeed); // set the parameter "speed" in the animator controler of "animator" to the same value as "currentSpeed"
                }
                else
                {
                    MoveOnAir(z);
                }
            }
           
        }
    }
    void MoveOnAir(float z) {
        
        if (z == 0 && jumpingCurrentSpeed - acceleration >= 0) // if there is not forward input decrease speed
        {
            jumpingCurrentSpeed -= acceleration;

        }
        else if (z == 0) // else set it to 0 if speed lower than speed - acceleration
        { 
            jumpingCurrentSpeed = 0;
        }
        if (z > 0) 
        {
            if (jumpingCurrentSpeed <= maxSpeed)
            {
                // continue accelerating 
                jumpingCurrentSpeed += acceleration;
            }
            else {
                // set speed equal to maximum speed
                jumpingCurrentSpeed = maxSpeed;
            }
            // if player has speed move him forward
            transform.Translate(new Vector3(0, 0, jumpingCurrentSpeed * Time.deltaTime ), Space.Self);
        }
    }
    void MoveOnGround(float z)
    {
       
        
        if (z == 0 && currentSpeed - acceleration >= 0)
        {
            // if player does not push the "forward" button, start decreasing speed
            currentSpeed -= acceleration;

        }
        else if (z == 0) {
            currentSpeed = 0;
        }

        if (z > 0)
        {
            if (currentSpeed <= maxSpeed)
            {
                // continue accelerating 
                currentSpeed += acceleration;
            }
            else
            {
                // set speed equal to maximum speed
                currentSpeed = maxSpeed;
            }
            // if player has speed move him forward
            transform.Translate(new Vector3(0, 0, currentSpeed * Time.deltaTime * enableMovemnt), Space.Self);
        }
    }

    public void EnableMovement(bool yesNo) {
        switch (yesNo) {
            case true:
                enableMovemnt = 1;
                break;
            case false:
                enableMovemnt = 0;
                break;
        }
    }
    public void SetCurrentSpeedToJumpingSpeed() {
        currentSpeed = jumpingCurrentSpeed;
    }
    public float GetCurrentSpeed() {
        return currentSpeed;
    }
    public void SetCurrentSpeed(float value) {
        currentSpeed = value;
    }
}