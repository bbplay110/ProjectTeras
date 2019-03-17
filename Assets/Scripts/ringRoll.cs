using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringRoll : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>().jet += 25;
        Destroy(gameObject,0);
        /*if (other.GetComponent<Player>().jet > 0)
        {
            other.GetComponent<Player>().SetJet(true);

        }*/
    }
}
