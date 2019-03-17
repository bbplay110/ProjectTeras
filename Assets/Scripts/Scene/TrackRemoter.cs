using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackRemoter : MonoBehaviour {
    public enum remoteType { up, down };
    public GameObject RemoteObject;
    public GameObject Tip;
    private ParticleSystem effect;
    
    public remoteType remote;
	// Use this for initialization
	void Start () {
        effect = transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
        Tip.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hInput.GetButtonDown("Submit") )
            {
                switch (remote)
                {
                    case remoteType.up:
                        iTween.MoveAdd(RemoteObject, new Vector3(0, 5, 0), 1 );
                        
                        break;

                    case remoteType.down:
                        iTween.MoveAdd(RemoteObject, new Vector3(0, -5, 0), 1);
                        break;
                }
                
            }
            
                
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Tip.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
            Tip.SetActive(false);
    }

}
