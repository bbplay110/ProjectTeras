using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBomb : MonoBehaviour {
    public GameObject corrosionGround;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public VirusBomb(float Explodesize,GameObject ThingToExplode)
    {
        ThingToExplode.transform.localScale *= Explodesize;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("SceneObject"))
        {
            GameObject TMPcorrosion_ground=Instantiate(corrosionGround, gameObject.transform.position, Quaternion.Euler(0,0,0))as GameObject;
            if (TMPcorrosion_ground.GetComponent<Spike>() != null)
                TMPcorrosion_ground.GetComponent<Spike>().Virus();

            Destroy(gameObject);
        }
        
    }
}
