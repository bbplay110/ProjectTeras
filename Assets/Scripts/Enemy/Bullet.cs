using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int DestoryTime;
	public int speed;
    public float power=60f;
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
        transform.Translate(new Vector3(0,0,1)*speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.name);
        if (hit.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.GetComponent<Player>().Body;
            hit.GetComponent<hurt>().damage(power, false, gameObject.transform);
            Debug.Log(hit.name);
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.GetMask("SceneObject"))
        {
            Destroy(gameObject);

        }
        
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject playerBody = hit.gameObject.GetComponent<Player>().Body;
            hit.gameObject.GetComponent<hurt>().damage(power, false, gameObject.transform);
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }
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
