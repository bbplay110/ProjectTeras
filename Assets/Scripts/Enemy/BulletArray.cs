using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArray : MonoBehaviour {
    public GameObject Bullet;
    public List<Transform> Position;
	// Use this for initialization
	void Start () {
		
	}
    public void shoot() {
        foreach (Transform i in Position) {
            Instantiate(Bullet, i.position, i.rotation);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
