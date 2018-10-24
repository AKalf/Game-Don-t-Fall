using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour {

    Animator animator;

    int pushTriggerHash = Animator.StringToHash("PushTrigger");
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	
    public void DoPush() {
        animator.SetTrigger(pushTriggerHash);
    }
}
