using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int DestoryTime;
	public int speed;
    public float power=60f;
    //public LayerMask layer;
    private float tempSpeed;
	// Use this for initialization
	void Start () {
        bulletTime.OnPauseTime += onBulletPause;
        bulletTime.UnPauseTime += unBulletPause;
        Destroy (gameObject, DestoryTime);

	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.Translate(new Vector3(0,0,1)*speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.gameObject;
            hit.GetComponent<hurt>().damage(power, false, gameObject.transform);
            //Debug.Log("HitThePlayer");
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.NameToLayer("SceneObject"))
        {
            //Debug.Log("HitWall");

            Destroy(gameObject);

        }
        
    }
    private void OnDestroy()
    {

        bulletTime.OnPauseTime -= onBulletPause;
        bulletTime.UnPauseTime -= unBulletPause;
    }
    void onBulletPause()
    {
        tempSpeed = speed;
        speed = 0;
        GetComponent<Collider>().enabled = false;
    }
    void unBulletPause()
    {
        speed = Mathf.FloorToInt(tempSpeed);
        GetComponent<Collider>().enabled = true;
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.gameObject;
            hit.gameObject.GetComponent<hurt>().damage(power, false, gameObject.transform);
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.NameToLayer("SceneObject"))
        {
            Destroy(gameObject);

        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
