using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2D :Bullet{
    public float TraceTime=3;
    //public float DistoryTime = 6;
    private Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}
    private void OnTriggerEnter(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update () {
        TraceTime -= 1 * Time.deltaTime;
        if (TraceTime >= 0)
        {
            iTween.LookUpdate(gameObject, iTween.Hash("axis","y","looktarget",player.position,"Time",0.5f));
        }
	}
}
