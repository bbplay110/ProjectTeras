using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkcube : MonoBehaviour {
    public GameObject SpiderMonster;
    public Transform playposition;
	// Use this for initialization
	void Start () {
        playposition = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(SpiderMonster, playposition.position, playposition.rotation);
        }
    }
}
