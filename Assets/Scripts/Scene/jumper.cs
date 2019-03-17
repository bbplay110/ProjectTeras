using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumper : MonoBehaviour {
    private float tmpJumpForce;
    public float JumpForce=50;
	// Use this for initialization
	void Start () {
       gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            tmpJumpForce = other.GetComponent<Player>().JumpHeight;
            other.GetComponent<Player>().JumpHeight = JumpForce;
        }
        Debug.Log("AAAAAAAAA");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().JumpHeight = tmpJumpForce;

        }
    }
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("BBBBBBB");
    }*/
}
