﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2IronBal : MonoBehaviour {
    public Transform followPoint;
    //public Transform IronFlyPoint;
    private bool followHand=true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (followHand) { 
        iTween.MoveUpdate(gameObject,followPoint.position, 0.3f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<hurt>().damage(100);
        }
    }
    public void ballfly(Vector3 PlayerPos)
    {
        followHand = false;
        iTween.MoveTo(gameObject,PlayerPos,1);
        Invoke("ballBack", 2);
    }
    void ballBack()
    {
        followHand = true;
    }
}
