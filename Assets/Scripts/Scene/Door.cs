using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int EnemysInSencer=1;
    public GameObject senser;
    public bool LaserDoor;
    private Animator Controller;
    public GameObject Tip;
    public GameObject Boss;
    private bool CanOpen=true;
    public bool BossDoor;
	// Use this for initialization
	void Start () {
        if(senser!=null)
            senser.GetComponent<MeshRenderer>().enabled = false;

        if (LaserDoor)
        {
            GetComponent<BoxCollider>().enabled = false;
            //gameObject.transform.position -= new Vector3(0, 0, 15);
            InvokeRepeating("check", 1, 1);
        }
        Tip.SetActive(false);
        Controller = GetComponent<Animator>();
	}
    
    /*private void OnTriggerEnter(Collider other)
    {
        
    }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            Tip.SetActive(true);
            if (hInput.GetButtonDown("Submit")&&CanOpen)
            {
                Controller.SetTrigger("open");
                if (BossDoor&&Boss!=null)
                {
                    Boss.SetActive(true);
                }
            }
            
        }
        
    }
    
    void check()
    {
        if (EnemysInSencer ==0)
        {
            //gameObject.transform.position += new Vector3(0, 0, 30);

            Controller.SetTrigger("open");
            //GetComponent<MeshRenderer>().enabled = true;
            CancelInvoke("check");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            Tip.SetActive(false);
        }
    }
    public void CanOpenDoor()
    {
        CanOpen = true;
    }
    public void CanNotOpenDoor()
    {
        CanOpen = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
