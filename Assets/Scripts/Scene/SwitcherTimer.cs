using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherTimer : MonoBehaviour {
    public GameObject[] switchObject;
    public float time = 1;
    private int a=0;
	// Use this for initialization
	void Start () {
        foreach (var item in switchObject)
        {
            item.SetActive(false);
        }
        switchObject[0].SetActive(true);
        InvokeRepeating("openObject",time,time);
	}
	void openObject()
    {
        switchObject[a % switchObject.Length].SetActive(false);
        a += 1;
        switchObject[a % switchObject.Length].SetActive(true);
    }
	// Update is called once per frame
}
