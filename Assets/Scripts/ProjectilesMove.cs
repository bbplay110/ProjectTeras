using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesMove : MonoBehaviour {
    public float speed;
    public float fireRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("NO Speed");
        }
	}
}
