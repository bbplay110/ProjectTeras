using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Manager : MonoBehaviour {
    public List<GameObject> triggerList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    public delegate void onSceneLoad();
    public static event onSceneLoad sceneloadAction;
    public GameObject player;
    
    public Flowchart Flowchart;

	// Use this for initialization
	void Start () {
        //sceneloadAction();
        player= GameObject.Find("Player");
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
