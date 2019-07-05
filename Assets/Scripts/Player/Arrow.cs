using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    Rigidbody rigi;
    public float damage = 20;
    public float ExitTime = 3;
    public string[] SticOn = new string[] {"Enemy","Boss","BreakableObject" };
    public GameObject Explotion;
	// Use this for initialization
	void Start () {
        rigi = GetComponent<Rigidbody>();
        rigi.AddForce(new Vector3(0,-5,0),ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in SticOn)
        {
            if (collision.collider.tag == item)
            {
                transform.parent = collision.collider.transform;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
