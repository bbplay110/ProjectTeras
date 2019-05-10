using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2D : Bullet {
    public float TraceTime=3;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update () {
        TraceTime -= 1 * Time.deltaTime;
        if (TraceTime >= 0)
        {


            iTween.LookUpdate(gameObject, iTween.Hash());
        }
	}
}
