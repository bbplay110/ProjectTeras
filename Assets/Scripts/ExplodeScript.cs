using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour {
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics.SphereCast(new Vector3(0,0,0),5,transform.forward,out hit))
        {
            hit.collider.GetComponent<ScriptA>().doSomething(gameObject.transform);
        }
	}
}
