using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforminter : MonoBehaviour {
    private Vector3 tempScale;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            tempScale = collision.transform.localScale;
            collision.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player") {

            collision.transform.parent = null;
            collision.transform.localScale = tempScale;
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
