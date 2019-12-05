using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaserCube : MonoBehaviour {
    private Animator ani;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
    }
	void onEnemyPause()
    {
        ani.speed = 0;
    }
    void unEnemyPause()
    {
        ani.speed = 1;
    }
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0, 15,0));
	}
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<hurt>().damage(99999,true,transform);
        }
    }
}
