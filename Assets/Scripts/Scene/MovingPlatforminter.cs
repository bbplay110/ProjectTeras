using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforminter : MonoBehaviour {
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player") { collision.transform.parent = null; }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
