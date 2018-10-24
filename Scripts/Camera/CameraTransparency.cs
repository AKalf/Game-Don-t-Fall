using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraTransparency : MonoBehaviour
{
    /*
     * Sets the material of the gameobjects between camera and player to transparent.
     * Only gameobjects at layer "Wall" are affected.
     * Sets the material back to the original when gameobjects are not any longer between camera and player.
     * Raycast is used for detection.
     */ 
    [SerializeField]
    Material trans;
    Dictionary<Transform, Material> objectsInFront = new Dictionary<Transform, Material>();
    public GameObject target;
    // Use this for initialization
    void Awake()
    {
        if (gameObject.name.StartsWith("P1"))
        {
            target = GameObject.FindGameObjectWithTag("PlayerBoy");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("PlayerGirl");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraClippingDetection();
    }
    void UseTransparentMaterial(Transform target)
    {
        Renderer targetRenderer;
        targetRenderer = target.GetComponent<Renderer>();
        targetRenderer.sharedMaterial = trans;
    }

    void UseNormalMaterial(Transform target, Material normalMat)
    {
        Renderer targetRenderer;
        targetRenderer = target.GetComponent<Renderer>();
        targetRenderer.sharedMaterial = normalMat;
    }


    void CameraClippingDetection()
    {
        
       
        //Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red, 0.01f);
        // Raycast from camera to player and store all the gameobjects that were hitted on "Wall" layer
        RaycastHit[] objHitted = Physics.RaycastAll(transform.position, target.transform.position - transform.position, Vector3.Distance(transform.position, target.transform.position), LayerMask.GetMask("Wall"), QueryTriggerInteraction.Collide);      
        if (objHitted.Length > 0)
        {
            foreach (RaycastHit hit in objHitted)
            {
                // if gameboject's material has not already been set to transparent
                if (objectsInFront.ContainsKey(hit.transform) == false)
                {
                    // copy gameobject's material
                    Material objMat = new Material(hit.transform.GetComponent<MeshRenderer>().sharedMaterial);
                    // add gameobject and it's material as a pair to the list of gameobjects whose material has been set to transparent
                    objectsInFront.Add(hit.transform, objMat);
                    // turn gameobject's material to transparent
                    UseTransparentMaterial(hit.transform);
                   
                }
            }
        }
        if (objectsInFront.Count > 0)
        {
            Transform[] keys = new Transform[objectsInFront.Keys.Count];
            // copy the list of gameobjects between camera and player to a new array
            objectsInFront.Keys.CopyTo(keys, 0); 
            for (int i = 0; i != keys.Length; i++) 
            {
                // if gameobject is not anymore hitted by raycast
                if (CheckIfRaycastContainsObject(keys[i], objHitted) == false)
                {
                    // set gameobject's material back to the original
                    UseNormalMaterial(keys[i], objectsInFront[keys[i]]);
                    // remove gameobject from the list
                    objectsInFront.Remove(keys[i]);
                }

            }
        }

    }
    bool CheckIfRaycastContainsObject(Transform objToCheck, RaycastHit[] objHitted)
    {
        for (int i = 0; i != objHitted.Length; i++)
        {
            if (objHitted[i].transform == objToCheck)
            {
                return true;
            }
        }
        return false;
    }
}