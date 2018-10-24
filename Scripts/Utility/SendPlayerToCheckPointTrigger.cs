using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPlayerToCheckPointTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("PlayerBoy"))
        {
            CheckPointManager.GetInstance().Respawn(other.gameObject, true);
        }
        else if (other.tag.StartsWith("PlayerGirl")) {
            CheckPointManager.GetInstance().Respawn(other.gameObject, false);
        }
    }
}
