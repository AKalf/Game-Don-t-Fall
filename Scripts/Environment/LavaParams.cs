using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaParams : MonoBehaviour {
    /*
     * Changes lava material properties on real time
     * Extends the "Substance" plugin using its API
     * Using SubstanceGraph class to access material properties
     */ 
    [SerializeField]
    Substance.Game.SubstanceGraph graph;

    [SerializeField]
    float maxLowTemp = 0.3f;
    [SerializeField]
    float minLowTemp = 0;


    bool increasingLowTemp = true;
	// Use this for initialization
	void Start () {
        //Substance.Game.SubstanceGraph.InputProperties[] properties = graph.GetInputProperties();
       
        //foreach (Substance.Game.SubstanceGraph.InputProperties prop in properties)
        //{
        //    Debug.Log(prop.label + " = " + prop.name);
           
        //}
    }
	
	// Update is called once per frame
	void Update () {
        Shift();
        ChangeTemperature();
    }

    // Makes lava texture to move
    private void Shift() {
        
        float previousValue = graph.GetInputFloat("fLowa");
        if (previousValue >= 1)
        {
            graph.SetInputFloat("fLowa", -1);
        }
        else {
            graph.SetInputFloat("fLowa", previousValue + 0.001f);
        }
        graph.QueueForRender();
    }
    // Change how hot the lava looks.
    private void ChangeTemperature() {

        // This part is used when the level is starting, to turn the texture from "cold-grey lava" to the final temperature
        float previousTempHigh = graph.GetInputFloat("lavatemphigh");
        if (previousTempHigh < 1)
        {
            graph.SetInputFloat("lavatemphigh", previousTempHigh + 0.001f);
        }

        // Increases and decreases how hot the lava looks
        float previousTempLow = graph.GetInputFloat("lavatemplow");
        if (increasingLowTemp)
        {
            if (previousTempLow <= maxLowTemp)
            {
                graph.SetInputFloat("lavatemplow", previousTempLow + 0.002f);
            }
            else
            {
                increasingLowTemp = false;
            }
        }
        else
        {
            if (previousTempLow >= minLowTemp)
            {
                graph.SetInputFloat("lavatemplow", previousTempLow - 0.002f);
            }
            else
            {
                increasingLowTemp = true;
            }
        }
        graph.QueueForRender();
    }

}
