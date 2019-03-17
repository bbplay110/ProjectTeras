using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongFishSensor : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = transform.parent.GetComponent<Animator>();
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
            animator.SetTrigger("Fire");
    }
}
