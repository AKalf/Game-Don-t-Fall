using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour {

    Material objectMaterial = null;
    Color initialColor;
    float colorModifier = 0.8f;
	// Use this for initialization
	void Start () {
        objectMaterial = GetComponent<MeshRenderer>().material;
        initialColor = objectMaterial.color;
	}
	
	// Update is called once per frame
	void Update () {
        ShadeObjectInRange();
    }
    private void ShadeObjectInRange()
    {

        try
        {
            // make the color a little bit whiter 
            objectMaterial.color += new Color(0.01f * colorModifier, +0.01f * colorModifier, +0.01f * colorModifier);
            if (objectMaterial.color.r >= 1 && objectMaterial.color.g >= 1 && objectMaterial.color.b >= 1)
            {
                // if color is white then start changing back to initial
                colorModifier = -1;
            }
            // if color is again same with the initial start changing to white
            else if (objectMaterial.color.r <= initialColor.r && objectMaterial.color.g <= initialColor.g && objectMaterial.color.b <= initialColor.b)
            {
                colorModifier = 1;
            }
        }
        // Catch the exception of object not having a material component or has the default
        catch (UnityException ex)
        {
            if (ex.GetBaseException().GetType() == typeof(MissingComponentException))
            {
                throw new UnityException("Error when trying to get interactable object material: " + objectMaterial.name + ". Make sure there is a material on the object and that it is not the default.");
            }
        }
    }
}
