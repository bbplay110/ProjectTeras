using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public GameObject Platform;
    public GameObject HitArea;
    public Transform point1;
    public Transform point2;
    public float MoveTime;
    public float WaitTime;
	// Use this for initialization
	void Start () {
        Invoke("MoveToPoint2", WaitTime);
        HitArea.GetComponent<MeshRenderer>().enabled=false;
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void MoveToPoint1()
    {
        Invoke("MoveToPoint2", WaitTime+MoveTime);
        iTween.MoveTo(Platform, iTween.Hash("position",point1.position,"time",MoveTime));
        
    }
    private void MoveToPoint2()
    {
        Invoke("MoveToPoint1", WaitTime + MoveTime);
        iTween.MoveTo(Platform, iTween.Hash("position", point2.position, "time", MoveTime));
    }
}
