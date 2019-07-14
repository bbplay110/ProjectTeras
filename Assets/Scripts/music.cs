using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {
    public AudioClip[] audios;
    private float clip = 1;
    private float ViewDist;
    private float Dis;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        /*
        Debug.Log("clip:"+clip + "Dis:" + Dis + "ViewDis:"+ViewDist);
        ViewDist = shootingEnemy.viewDist;
        Dis = shootingEnemy.Dis;
        if (clip != 1 && Dis <= ViewDist)
        {
            this.GetComponent<AudioSource>().clip = audios[1];
            this.GetComponent<AudioSource>().Play();
            clip = 1;
        }
        if (clip != 0 && Dis >= ViewDist)
        {
            this.GetComponent<AudioSource>().clip = audios[0];
            this.GetComponent<AudioSource>().Play();
            clip = 0;
        }*/
    }
}
