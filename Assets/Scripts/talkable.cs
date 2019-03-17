using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class talkable : MonoBehaviour {
    public Flowchart talkFlowchart;
    public string onCollosionEnter;
    public GameObject Tip;
    public static Flowchart flowchartManager;
    // Use this for initialization
    void Awake ()
    {
        flowchartManager = GameObject.Find("對話管理器").GetComponent<Flowchart>();
        Tip.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Tip.SetActive(true);
            if (Input.GetButtonDown("Submit"))
            {
                Block targetblock = talkFlowchart.FindBlock(onCollosionEnter);
                talkFlowchart.ExecuteBlock(targetblock);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Tip.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update () {

	}

public  bool isTalking
    {
        get { return flowchartManager.GetBooleanVariable("對話中"); }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Block targetblock = talkFlowchart.FindBlock(onCollosionEnter);
            talkFlowchart.ExecuteBlock(targetblock);
        }
    }*/
    /*private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Block targetblock = talkFlowchart.FindBlock(onCollosionEnter);
            talkFlowchart.ExecuteBlock(targetblock);
        }
    }*/
}