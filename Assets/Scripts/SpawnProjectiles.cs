using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public RotateToMouse rotateToMpawn;
    private int realVfx;
    private GameObject effectToSpawn;

	// Use this for initialization
	void Start () {
        realVfx = 0;
        effectToSpawn = vfx[realVfx];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnVFX ();
        }
        if (Input.GetMouseButtonDown(1)) {
            realVfx += 1;
            if (realVfx > vfx.Count - 1)
            {
                realVfx = (realVfx % vfx.Count)-1;
            }
            effectToSpawn = vfx[realVfx];
        }
	}

    void SpawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (rotateToMpawn!=null)
            {
                vfx.transform.localRotation = rotateToMpawn.GetRotation();
            }
        }
        else
        {
            Debug.Log("NO FIRE POINT");
        }


    }
}
