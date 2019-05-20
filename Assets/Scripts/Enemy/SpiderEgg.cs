using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEgg : MonoBehaviour {
    private GameObject Player;
    private float Dis;
    public float SpawnSpiderDis=8;
    public GameObject Spider;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Dis =Vector3.Distance(transform.position,Player.transform.position);
        if (Dis <= SpawnSpiderDis)
        {
            GameObject sSpider=Instantiate(Spider, transform.position,Quaternion.Euler(0,0,0))as GameObject;
            if (sSpider.GetComponent<BoxCollider>() != null) { 
                sSpider.GetComponent<BoxCollider>().enabled = false;
                sSpider.GetComponent<BoxCollider>().isTrigger = false;
            }
            else
            {
                sSpider.GetComponent<CapsuleCollider>().enabled = false;
                sSpider.GetComponent<CapsuleCollider>().isTrigger = false;
            }
            sSpider.GetComponent<NavMeshAgent>().enabled = false;
            sSpider.GetComponent<SpiderMonster>().Invoke("Spawn", 0.5f);
            Destroy(gameObject);
        }
	}
}
