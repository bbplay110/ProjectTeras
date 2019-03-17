using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AID : MonoBehaviour {
    public float healthPlus=100;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAAAAAAAA");
        if (other.tag == "Player")
        {
            other.GetComponent<hurt>().damage(-healthPlus);
            Destroy(gameObject);
            
        }
    }
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0,2,0));
	}
}
