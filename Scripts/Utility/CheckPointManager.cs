using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    /*
     * Stores the checkpoints of players
     */ 

    static CheckPointManager inst = null;
    public Vector3 currentBoyCheckPointPos;
    public Vector3 currentGirlCheckPointPos;


    private void Awake()
    {
        if (inst == null) {
            inst = this;
        }
    }
    public static CheckPointManager GetInstance() {
        if (inst == null)
        {
            inst = new CheckPointManager();
            return inst;
        }
        else {
            return inst;
        }
    }
    public void Respawn(GameObject player, bool boy) {
        if (boy)
        {
            player.transform.position = currentBoyCheckPointPos;
        }
        else {
            player.transform.position = currentGirlCheckPointPos;
        }
       
    }
    
   
}
