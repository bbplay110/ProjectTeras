using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2D :MonoBehaviour{
    public float TraceTime=3;
    private Transform player;
    private bool isHit=false;
    public int DestoryTime;
    public int speed;
    public float power = 60f;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
	}
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.name);
        if (hit.tag == "Player")
        {
            GameObject playerBody = hit.GetComponent<Player>().Body;
            hit.GetComponent<hurt>().damage(power, false, gameObject.transform);
            Debug.Log(hit.name);
            if (power > 100)
            {
                playerBody.GetComponent<Animator>().SetTrigger("damage");
            }

            Destroy(gameObject);
        }
        else if (hit.gameObject.layer == LayerMask.GetMask("SceneObject"))
        {
            //Destroy(gameObject);

        }
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        TraceTime -= 1 * Time.deltaTime;
        if (TraceTime >= 0 && !isHit)
        {
            iTween.LookUpdate(gameObject, iTween.Hash("axis", "y", "looktarget", player.position, "Time", 0.5f));
        }

	}
}
