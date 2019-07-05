using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleD : MonoBehaviour {
    // Use this for initialization
    public Transform followPoint;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}
    private void move()
    {
        iTween.MoveUpdate(gameObject, followPoint.position, 0.5f);
    }
}
