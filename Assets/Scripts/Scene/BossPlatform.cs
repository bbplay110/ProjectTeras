using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPlatform : MonoBehaviour {
    public GameObject Platform;
    public GameObject HitArea;
    public Transform point1;
    public Transform point2;
    public float MoveTime;
    public float WaitTime;
	// Use this for initialization
	void Start () {
        HitArea.GetComponent<MeshRenderer>().enabled=false;
        Platform.transform.position = point1.position;
    }

    // Update is called once per frame
    void Update () {
        
    }
    public void MoveToPoint2()
    {
        iTween.MoveTo(Platform, iTween.Hash("position", point2.position, "time", MoveTime));

        
    }
}
