using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectByTag : MonoBehaviour {
    int a = 0;
	// Use this for initialization
	void Start () {
        InvokeRepeating("RotateA",0,1);
	}
	public void RotateA()
    {
        //做事
        a += 1;
        if (a >= 90)
        {
            CancelInvoke("RoteatA");
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
