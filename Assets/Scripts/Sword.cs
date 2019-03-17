using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    public string DamageTag;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider Enemya){
		if (Enemya.tag==DamageTag)
        {
			Enemya.GetComponent<Player> ().Player1.SetTrigger ("damage");
			Enemya.GetComponent<hurt>().damage(50);
		}
	}
}
