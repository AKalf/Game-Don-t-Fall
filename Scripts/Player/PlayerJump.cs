using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

   
    [SerializeField]
    GameObject LeftFoot = null;
    [SerializeField]
    GameObject RightFoot = null;
    [SerializeField]
    float jumpForce = 100f; 
    [SerializeField]
    float animationRunJumpOffset = 0.2f;
    Rigidbody rb;

   
    Animator animator;
    PlayerMovement playerMovementComp = null;
    AudioSource playerAudioSource = null;
    int jumpTriggerHash = Animator.StringToHash("jumpTrigger");
    int groundedBoolHash = Animator.StringToHash("Grounded");
    bool canJump = true;
    bool coroutineRunning = false;
    bool grounded = true;
    int playerNumber = -1;

    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
        playerMovementComp = GetComponent<PlayerMovement>();
        playerAudioSource = GetComponent<AudioSource>();
        
    }
   
    // Update is called once per frame
    void Update() {
        
        if (PlayerMovement.GetInput)
        {
            if (!coroutineRunning)
            {
                bool isOnAir = !grounded; // get the state of the player before checking for ground
                RaycastHit hit;
               // if a raycast from left foot or right foot hit a gameobject on layer "Ground"
                if (Physics.Raycast(LeftFoot.transform.position, -transform.up, out hit, 0.1f, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) || Physics.Raycast(RightFoot.transform.position, -transform.up, out hit, 0.1f, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore))
    
                {
                    if (hit.transform.position.y - transform.position.y < 0.1f)
                    {

                        grounded = true;
                        // if before doing the raycast player was on air and after raycast player is grounded
                        if (isOnAir == true && grounded == true)
                        {
                            // means player has landed. Start landing
                            StartCoroutine(beAbleToJumpAgain());
                        }
                    }
                    if ((Input.GetKeyDown(KeyCode.Space)) && grounded && playerNumber == 1 && !coroutineRunning && canJump)
                    {
                        canJump = false;
                        coroutineRunning = true;
                        // tell animator to play "startJump" animation
                        animator.SetTrigger(jumpTriggerHash);
                        // tell PlayerAudioManager to play "jump" sound
                        MessageDispatch.GetInstance().SendAudioMessageForDispatch("Jump", playerAudioSource);
                        StartCoroutine("Jump");

                    }
                    else if ((Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown("joystick button 0")) && grounded && playerNumber == 2 && !coroutineRunning && canJump)
                    {
                        canJump = false;
                        coroutineRunning = true;
                        animator.SetTrigger(jumpTriggerHash);
                        MessageDispatch.GetInstance().SendAudioMessageForDispatch("Jump", playerAudioSource);
                        StartCoroutine("Jump");
                    }
                }
                else
                {
                    grounded = false;
                }

            }
            animator.SetBool(groundedBoolHash, grounded);
        }

    }
    IEnumerator Jump()
    {   
        animator.SetBool(groundedBoolHash, false);
        yield return new WaitForSeconds(animationRunJumpOffset); // wait to synchronize with animation if needed. 
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        grounded = false;
        yield return new WaitForSeconds(0.1f); // wait before raycast for ground
        coroutineRunning = false;
    }

    IEnumerator beAbleToJumpAgain() {
        playerMovementComp.SetCurrentSpeed(0); // make player stop moving and reset movement
        playerMovementComp.EnableMovement(false); // disable movement so he stays in place
        animator.SetBool(groundedBoolHash, true); // play "Landing" animation
        coroutineRunning = true;
        yield return new WaitForSeconds(0.4f); // wait for animation to end
        OnLanding();
    }

    void OnLanding() {
        canJump = true;   
        coroutineRunning = false;
        playerMovementComp.EnableMovement(true);
    }
    public bool GetGrounded() {
        return grounded;
    }
}
