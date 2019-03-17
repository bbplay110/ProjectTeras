using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBodyRotain2 : MonoBehaviour {
    public GameObject cameraTran;
    private Vector2 input;
    public float rotaSpeed = 0.5f;
    private float yvector = 0.0f;
    // Use this for initialization
    
    void Start () {    
    }
	
	// Update is called once per frame
	void Update () {
        cameraTran = GameObject.Find("CameraDir");
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") !=0||Input.GetAxis("Vertical") !=0) {

        }

    }
    public void smoothRotateY(float iTargetAngle)
    {
    }
}
