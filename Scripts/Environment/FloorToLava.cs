using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorToLava : MonoBehaviour {

    /* 
     * Increase fire-lights intensity 
     * Inceases the lava-floor material light
    */
    [SerializeField]
    List<Light> fireLights = new List<Light>(); // the fire-lights 

    float[] intensityValues; // stores the final intensity values of fire-lights

    
    
    
    // Use this for initialization
    void Start () {
        intensityValues = new float[fireLights.Count];
        int index = 0;
        // for each fire-light 
        foreach (Light light in fireLights) {
            // store the starting value 
            intensityValues[index] = light.intensity;
            // change it to 0
            light.intensity = 0;
            // increase index
            index++;
        }
	}
	
	// Update is called once per frame
	void Update () {
       
        bool allLightsSet = true; // checks if all fire-lights intensity has been increase to starting value
        // for each fire-light
        for (int index = 0; index != fireLights.Count; index++) {
            // if current intensity still smaller than starting value
            if (fireLights[index].intensity < intensityValues[index] )
            {
                // increase intensity
                fireLights[index].intensity += 0.005f;
                // at least 1 fire-light intensity is not equal to its starting value
                allLightsSet = false;
            }               
        }
        // if all fire-lights reached their starting intensity value
        if (allLightsSet) {          
            // Destroy this gameobject
            Destroy(this.gameObject);
        }
        
	}
    
    
}
