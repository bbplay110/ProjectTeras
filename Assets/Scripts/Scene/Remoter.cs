using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remoter : MonoBehaviour {
    public GameObject RemoteObject;
    public GameObject Tip;
    public GameObject Tip2;
    public int RemoterToOpen=5;
    public bool Main = false;
    private bool IsOpen=false;
    private ParticleSystem effect;
	// Use this for initialization
	void Start () {
        effect = transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
        Tip.SetActive(false);
        if(Tip2!=null)
            Tip2.SetActive(false);
        if (Main)
            RemoteObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if (hInput.GetButtonDown("Submit") && Main && RemoterToOpen <= 0&&!IsOpen)
            {
                IsOpen = true;
                effect.startColor = Color.blue;
                RemoteObject.SetActive(true);
            }
            else if (hInput.GetButtonDown("Submit") && !Main&&!IsOpen)
            {
                IsOpen = true;
                effect.startColor = Color.blue;
                RemoteObject.GetComponent<Remoter>().RemoterToOpen -= 1;
            }
            
                
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&RemoterToOpen<=0)
            Tip.SetActive(true);

        if (other.tag == "Player" && RemoterToOpen > 0 && Main)
            Tip2.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
            Tip.SetActive(false);
        if (other.tag == "Player" && RemoterToOpen > 0 && Main)
            Tip2.SetActive(false);
    }

}
