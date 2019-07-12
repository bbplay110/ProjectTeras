using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    Rigidbody rigi;
    private bool startDamage;
    public float damage = 20;
    public float ExitTime = 3;
    public string[] SticOn = new string[] {"Enemy","Boss","BreakableObject" };
    public GameObject Explotion;
	// Use this for initialization
	void Start () {
        rigi = GetComponent<Rigidbody>();
        rigi.AddForce(transform.forward*20,ForceMode.Impulse);
       
	}
	
	// Update is called once per frame
	void Update () {
        ExitTime -= 1 * Time.deltaTime;
        if (ExitTime <= 0)
        {
            Destroy(gameObject);
        }
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer==LayerMask.NameToLayer("SceneObject"))
        {
            Instantiate(Explotion, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), null);
            Destroy(gameObject);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            transform.SetParent(collision.transform);
            rigi.useGravity = false;
            rigi.isKinematic = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in SticOn)
        {
            if (other.tag == item)
            {
                transform.parent =other.transform;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
