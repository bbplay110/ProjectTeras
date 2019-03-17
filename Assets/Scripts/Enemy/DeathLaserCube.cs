using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaserCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 15,0));
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<hurt>().damage(99999);
        }
    }
}
