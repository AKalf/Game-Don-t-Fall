using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumberOfPlayersButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetNumber(bool p2) {
        P2Settings.SetNumberOfPlayers(p2);
    } 
}
