using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2D :MonoBehaviour{
    public float TraceTime=3;
    private Transform player;
    private bool isHit=false;
    public float DestoryTime;
    public int speed;
    public float power = 60f;
    private bool isPaused;
    // Use this for initialization
    void Start () {
        bulletTime.OnPauseTime += onPause;
        bulletTime.UnPauseTime += unPause;
        player = GameObject.Find("Player").transform;
	}
    void onPause()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Pause();
        isPaused = true;
        GetComponent<Collider>().enabled = false;
    }
    void unPause()
    {

        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        isPaused = false;

        GetComponent<Collider>().enabled = true;
    }
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.name);
        if (hit.tag == "Player")
        {
            GameObject playerBody = hit.gameObject;
            hit.GetComponent<hurt>().damage(power, false, gameObject.transform);
            Debug.Log(hit.name);
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }

            Destroy(gameObject);
            //iTween.ShakePosition(Camera.main.gameObject, new Vector3(0, 0,0), 1.0f);
            //Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.GetMask("SceneObject"))
        {
            //Destroy(gameObject);

        }
    }
    // Update is called once per frame
    void Update () {
        if(!isPaused)
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        TraceTime -= 1 * Time.deltaTime;
        if (TraceTime >= 0 && !isHit)
        {
            iTween.LookUpdate(gameObject, iTween.Hash("axis", "y", "looktarget", player.position, "Time", 0.5f));
        }
        DestoryTime -= 1 * Time.deltaTime;
        if (DestoryTime <= 0)
        {
            Destroy(gameObject, 0);
        }

	}
    private void OnDestroy()
    {

        bulletTime.OnPauseTime -= onPause;
        bulletTime.UnPauseTime -= unPause;
    }
}
