using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    private Transform Player;
    public float Speed;
    public float Damage;
	// Use this for initialization
	void Start () {
        Player=GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        float forwardSpeed = Speed * Time.deltaTime;
        transform.Translate(Vector3.forward * forwardSpeed);
        turnToPlayer();
	}
    void turnToPlayer()
    {
        transform.LookAt(Player.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<hurt>().damage(Damage);            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        
    }
}
