using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoorSenser : MonoBehaviour {

    public GameObject Door;
    private bool triggered;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        
        if (other.tag=="MiddleBoss"||(other.tag == "Enemy" && other.GetComponent<hurt>().needCleared))
        {
            Door.GetComponent<Door>().EnemysInSencer++;
        }
        
    }
    


    void Update () {
	}
    
}
