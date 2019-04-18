using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int DestoryTime;
	public int speed;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, DestoryTime);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.Translate(transform.forward*speed*Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.GetComponent<Player>().Body;
            playerBody.GetComponent<Animator>().SetTrigger("damage");
            hit.GetComponent<hurt>().damage(60f, false, gameObject.transform);
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.GetMask("SceneObject"))
        {
            Destroy(gameObject);

        }
        else
        {
            //Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.gameObject.GetComponent<Player>().Body;
            playerBody.GetComponent<Animator>().SetTrigger("damage");
            hit.gameObject.GetComponent<hurt>().damage(60f, false, gameObject.transform);
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.GetMask("SceneObject"))
        {
            Destroy(gameObject);

        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
