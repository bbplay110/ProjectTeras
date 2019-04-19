using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int EnemysInSencer=1;
    //public GameObject senser;
    public bool LaserDoor;
    private Animator Controller;
    public GameObject Tip;
    public GameObject Boss;
    public bool BossDoor;
    private bool isOpen = false;
	// Use this for initialization
	void Start () {
        /*if(senser!=null)
            senser.GetComponent<MeshRenderer>().enabled = false;*/

        if (LaserDoor)
        {
            GetComponent<BoxCollider>().enabled = false;
            //gameObject.transform.position -= new Vector3(0, 0, 15);
            InvokeRepeating("check", 1, 1);
        }
        Tip.SetActive(false);
        Controller = GetComponent<Animator>();
	}
    public void InSwitch()
    {
        Tip.SetActive(true);
        if (hInput.GetButtonDown("Submit"))
        {
            ChengeState();
        }
    }
    public void ExitSwitch()
    {
        Tip.SetActive(false);
    }
    public void open()
    {
        if (isOpen == false)
        {
            Controller.SetTrigger("open");
            isOpen = true;
        }
    }
    public void close()
    {
        if (isOpen == true)
        {
            Controller.SetTrigger("open");
        }
    }
    void ChengeState()
    {
        isOpen = !isOpen;
        Controller.SetTrigger("open");
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
    // Update is called once per frame
    void Update () {
		
	}
}
