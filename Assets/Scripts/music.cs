using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {
    public AudioClip[] audios = new AudioClip[2];
	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("B"))
        {
            
        }
    }
}
