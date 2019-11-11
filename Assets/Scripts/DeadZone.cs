using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    public Transform SpawnPoint;
    public bool hide = true;
	// Use this for initialization
	void Start () {
        if (hide && GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider Player)
    {
        Player.gameObject.transform.position = SpawnPoint.position;
    }
}
