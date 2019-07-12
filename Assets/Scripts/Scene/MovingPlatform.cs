using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public GameObject Platform;
    public GameObject HitArea;
    public Transform point1;
    public Transform point2;
    public iTween.EaseType easeType=iTween.EaseType.linear;
    public float MoveTime;
    public float WaitTime;
    private float tempMoveTime, tempWaitTime;
    private string tempVoid= "MoveToPoint1";
    private bool isPaused=false;
	// Use this for initialization
	void Start () {
        if(HitArea!=null)
            HitArea.GetComponent<MeshRenderer>().enabled=false;
        bulletTime.OnPauseTime += OnPauseAction;
        bulletTime.UnPauseTime += UnPauseAction;
    }

    // Update is called once per frame
    void Update () {
        if (!isPaused&& tempMoveTime>0)
        {
            tempMoveTime -= 1 * Time.deltaTime;
        }
        else if (!isPaused && tempMoveTime <= 0 && tempWaitTime > 0)
        {
            tempWaitTime -= 1 * Time.deltaTime;
        }

        if (WaitTime <= 0)
        {
            if(tempVoid== "MoveToPoint1")
            {
                tempVoid = "MoveToPoint2";
            }
            else if (tempVoid == "MoveToPoint2")
            {
                tempVoid = "MoveToPoint1";
            }
            Invoke(tempVoid, 0);
            tempMoveTime = MoveTime;
            tempWaitTime = WaitTime;
        }
    }
    void OnPauseAction()
    {
        isPaused = true;
        iTween.Pause(gameObject);
    }
    void UnPauseAction()
    {
        isPaused = false;
        iTween.Resume(gameObject);
    }
    private void MoveToPoint1()
    {
        iTween.MoveTo(Platform, iTween.Hash("position",point1.position,"time",MoveTime, "easeType",easeType));
        
    }
    private void MoveToPoint2()
    {
        iTween.MoveTo(Platform, iTween.Hash("position", point2.position, "time", MoveTime, "easeType", easeType));
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= OnPauseAction;
        bulletTime.UnPauseTime -= UnPauseAction;
    }
}
