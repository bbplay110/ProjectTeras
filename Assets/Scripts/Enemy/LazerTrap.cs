using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTrap : MonoBehaviour {
    private Ray Lazer;
    public Transform eye;
    public float tmpRange = 999;
    public int Layer;
    private LineRenderer LINE;
    private hurt player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<hurt>();
        LINE = GetComponent<LineRenderer>();
        Lazer.origin = eye.position;
        Lazer.direction = eye.forward;
        Layer = LayerMask.NameToLayer("SceneObject");
        LINE.SetPosition(0, eye.position);
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit Hit;

        if (Physics.Raycast(Lazer, out Hit,tmpRange))
        {
            
            LINE.SetPosition(1, Hit.point);
            if (Hit.transform.gameObject.layer ==Layer)
            {
                
                tmpRange = Vector3.Distance(eye.position,Hit.transform.position);
            }
            else if (Hit.transform.tag == "Player")
            {

                player.damage(0.2f,true,Hit.transform);
            }
                

        }
        Debug.DrawLine(eye.position, eye.position - Hit.transform.position, Color.blue, 0.1f);

    }
}
