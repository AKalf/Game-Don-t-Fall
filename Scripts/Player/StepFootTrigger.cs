using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFootTrigger : MonoBehaviour {

    /*
     * Checks the floor tag for material info
     * Informs PlayerAudioListener for the material that player is stepping on
     */ 
    FloorMat floorMat;
    int groundMaskIndex;

    AudioSource aSource;  
	// Use this for initialization
	void Start () {
        groundMaskIndex = LayerMask.GetMask("Ground");
        aSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;        
        Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundMaskIndex);
        if (hit.transform != null) {
            if (hit.transform.tag == "Wood" && floorMat != FloorMat.wood)
            {
                floorMat = FloorMat.wood;
                MessageDispatch.GetInstance().SendAudioMessageForDispatch("SetWood", aSource);
            }
            else if (hit.transform.tag == "Fabric" && floorMat != FloorMat.fabric) {
                floorMat = FloorMat.fabric;
                MessageDispatch.GetInstance().SendAudioMessageForDispatch("SetFabric", aSource);
            }
                
        }
        
	}
    public void SendFootStepMessage() {
        MessageDispatch.GetInstance().SendAudioMessageForDispatch("Run", aSource);
    }
}
