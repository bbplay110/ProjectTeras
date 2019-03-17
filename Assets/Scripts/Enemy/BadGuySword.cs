using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuySword : MonoBehaviour {
    private GameObject PlayerBody;
    public float Damage = 10;
    public bool Hide = true;
	// Use this for initialization
	void Start () {
        PlayerBody=GameObject.Find("Player_Body");
        if (GetComponent<MeshRenderer>() != null && Hide) 
            GetComponent<MeshRenderer>().enabled = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.GetComponent<hurt>().damage(Damage);
            PlayerBody.GetComponent<Animator>().SetTrigger("damage");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
