using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    Rigidbody rigi;
    private bool startDamage;
    public float damage = 20;
    public float ExitTime = 3;
    private float currentExitTime;
    private Vector3 tempVelocity;
    private bool isPaused;
    private bool pauseOnStart = false;
    public string[] SticOn = new string[] {"Enemy","Boss","BreakableObject" };
    public GameObject Explotion;
    private hurt EnemyHurt;
    // Use this for initialization
    void Start () {
        isPaused = GameObject.Find("Player").GetComponent<bulletTime>().IsPaused;

        
        rigi = GetComponent<Rigidbody>();
        rigi.AddForce(transform.forward*3,ForceMode.Impulse);

        bulletTime.OnPauseTime += onPauseEvent;
        bulletTime.UnPauseTime += unPauseEvent;
        if (isPaused)
        {

            this.onPauseEvent();
            pauseOnStart = true;
        }
    }
	void onPauseEvent()
    {
        isPaused = true;
        GetComponent<Collider>().enabled = false;
        tempVelocity = rigi.velocity;
        rigi.Sleep();
    }
    void unPauseEvent()
    {
        isPaused = false;
        GetComponent<Collider>().enabled = true;
        rigi.WakeUp();
        if (pauseOnStart)
        {
            rigi.AddForce(transform.forward * 3, ForceMode.Impulse);
            pauseOnStart = false;
        }
        else
        {
            rigi.AddForce(tempVelocity);
        }

        
    }
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            ExitTime -= 1 * Time.deltaTime;
        }
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
            EnemyHurt = collision.gameObject.GetComponent<hurt>();
            InvokeRepeating("hurtEnemy",0,0.2f);

        }
    }

    void hurtEnemy()
    {
        if(EnemyHurt!=null)
            EnemyHurt.damage(damage/ExitTime,true);
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onPauseEvent;
        bulletTime.UnPauseTime -= unPauseEvent;
    }
}
