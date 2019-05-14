using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent1 : MonoBehaviour {
    public UnityEvent events;
	// Use this for initialization
	void Start () {
		
	}
	public void callEvent()
    {
        events.Invoke();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
