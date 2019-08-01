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
        Invoke(tempVoid, 0);
        if(HitArea!=null)
            HitArea.GetComponent<MeshRenderer>().enabled=false;
        bulletTime.OnPauseTime += OnPauseAction;
        bulletTime.UnPauseTime += UnPauseAction;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("tempWaitTime="+tempWaitTime+"TempMoveTime="+tempMoveTime);
        //Debug.Log("tempWaitTime="+tempWaitTime+"TempMoveTime="+tempMoveTime);
        if (!isPaused&& tempMoveTime>0)
        {
            tempMoveTime -= 1 * Time.deltaTime;
        }
        else if (!isPaused && tempMoveTime <= 0 && tempWaitTime > 0)
        {
            tempWaitTime -= 1 * Time.deltaTime;
        }

        else if (tempWaitTime <= 0&& tempMoveTime <= 0&&!isPaused)
        {
            tempMoveTime = MoveTime;
            tempWaitTime = WaitTime;
            if(tempVoid== "MoveToPoint1")
            {
                tempVoid = "MoveToPoint2";
            }
            else if (tempVoid == "MoveToPoint2")
            {
                tempVoid = "MoveToPoint1";
            }
            Invoke(tempVoid, 0);
        }

    }
    void OnPauseAction()
    {
        isPaused = true;
        iTween.Pause(Platform);
    }
    void UnPauseAction()
    {
        isPaused = false;
        iTween.Resume(Platform);
    }
    private void MoveToPoint1()
    {
        //Debug.Log(tempVoid);
        iTween.MoveTo(Platform, iTween.Hash("position",point1.position,"time",MoveTime, "easeType",easeType));
        
    }
    private void MoveToPoint2()
    {
        //Debug.Log(tempVoid);
        iTween.MoveTo(Platform, iTween.Hash("position", point2.position, "time", MoveTime, "easeType", easeType));
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= OnPauseAction;
        bulletTime.UnPauseTime -= UnPauseAction;
    }
}
