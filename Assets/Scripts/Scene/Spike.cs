using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    public float damage = 99999; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<hurt>().damage(damage);
        }
    }
    public void Virus()
    {
        Destroy(gameObject, 7);
    }
}
