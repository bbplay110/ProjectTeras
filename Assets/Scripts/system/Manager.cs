using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Manager : MonoBehaviour {

    public Flowchart Flowchart;
	// Use this for initialization
	void Start () {
		
	}
	void Talk(Block block)
    {
        Time.timeScale = 0;
        block.Execute();

    }
	// Update is called once per frame
	void Update () {
		
	}
}
