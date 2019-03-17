using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Drone: MonoBehaviour {
    public GameObject Bomb;
    private bool dropBomb;
    public float rate=3;
    public AnimationCurve curve;
    public Transform followPoint;
    public bool DropBomb
    {
        get
        {
            return dropBomb;
        }

        set
        {
            dropBomb = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //iTween.MoveUpdate(iTween.Hash("",""));
	}
}
