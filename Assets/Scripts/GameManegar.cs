using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManegar : MonoBehaviour {
    public List<Transform> target = new List<Transform>();
    // Use this for initialization
    void Start () {
        foreach (GameObject aaa in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            target.Add(aaa.transform);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
