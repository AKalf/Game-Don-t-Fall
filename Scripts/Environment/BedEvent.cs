using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedEvent : MonoBehaviour, IEventListener {

    /*
     * This component is placed on a trigger collider, on the bed.
     * Upon collision with player, it sets active player's 3d text
     * if player presses "interaction button" while in collider, the pillow on the bed is pushed.
     */ 
    [SerializeField]
    GameObject pillow = null;
    [SerializeField]
    float pushAnimationOffset = 0.1f;
    GameObject boy = null;
    GameObject girl = null;
    bool eventStarted = false;

    // Use this for initialization
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (!eventStarted)
        {
            if (boy != null && (Input.GetKey(KeyCode.E)))
            {
                StartCoroutine(StartEvent(true));
            }
            else if (girl != null && Input.GetKeyDown("joystick button 2"))
            {
                StartCoroutine(StartEvent(false));
            }
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        // set playerBoy's 3d text as active
        if (collision.transform.tag == "PlayerBoy") {
            TextMesh playerText = collision.transform.GetChild(0).GetComponentInChildren<TextMesh>();
            playerText.text = "Press: E";
            boy = collision.gameObject;
        }
        // set playerGirl's 3d text as active
        else if (collision.transform.tag == "PlayerGirl")
        {
            TextMesh playerText = collision.transform.GetComponentInChildren<TextMesh>();
            playerText.text = "Press: X";
            girl = collision.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        // set playerBoy's 3d text as inactive
        if (collision.transform.tag == "PlayerBoy")
        {
            TextMesh playerText = collision.transform.GetComponentInChildren<TextMesh>();
            playerText.text = "";
            boy = null;
        }
        // set playerBoy's 3d text as inactive
        else if (collision.transform.tag == "PlayerGirl")
        {
            TextMesh playerText = collision.transform.GetComponentInChildren<TextMesh>();
            playerText.text = "";
            girl = null;
        }
    }
    public IEnumerator StartEvent(bool isBoy) {
        eventStarted = true;
        // tell the player that triggered the event to do the "Push" animation
        if (isBoy)
        {
            boy.transform.LookAt(pillow.transform);
            boy.GetComponent<Pushing>().DoPush();
            boy.transform.GetComponentInChildren<TextMesh>().text = "";
        }
        else {
            girl.transform.LookAt(pillow.transform);
            girl.GetComponent<Pushing>().DoPush();
            girl.transform.GetComponentInChildren<TextMesh>().text = "";
        }
        
        Destroy(pillow.transform.GetChild(0).gameObject); // destroy the light with particles
        Destroy(pillow.GetComponent<HighlightObject>()); // destroy the component that makes pillow highlight
        yield return new WaitForSeconds(pushAnimationOffset); // synchronize with animation
        pillow.GetComponent<Rigidbody>().isKinematic = false; // set pillow's rb to non-kinematic
        pillow.GetComponent<Rigidbody>().AddForce(-transform.up * 6.5f, ForceMode.Impulse); // add force to the pillow so it falls in lava
        Destroy(this.gameObject); // we are done with this component
    }
   
    
}
