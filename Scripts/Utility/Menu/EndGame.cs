using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour {

    bool girlPlaying = false;
    bool girlOn = false;
    bool boyOn = false;
    

    // Use this for initialization
    void Start () {
        girlPlaying = P2Settings.GetPlayers();
    }
	
	// Update is called once per frame
	void Update () {
        if (girlPlaying)
        {
            if (boyOn && girlOn)
            {
                InGameMenu.GetInstance().Finish();
            }
        }
        else {
            if (boyOn) {
                InGameMenu.GetInstance().Finish();
            }
        }
        
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBoy")
        {
            boyOn = true;
        }
        else if (other.tag == "PlayerGirl") {
            girlOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerBoy")
        {
            boyOn = false;
        }
        else if (other.tag == "PlayerGirl")
        {
            girlOn = false;
        }
    } 
}
