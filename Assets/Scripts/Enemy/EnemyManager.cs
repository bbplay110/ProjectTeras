using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public GameObject Enemy;
    public float delayTime = 1f;
    public float repeatRate = 3f;
    public Transform[] SpawnPoint;
    private bool PlayerIsDeath;
    // Use this for initialization
    private void PlayerDeathAction()
    {
        PlayerIsDeath = true;

    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
