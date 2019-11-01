using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHide : MonoBehaviour {
    private Transform main;
    private Renderer renderer1;
    private Material[] materials;
    private void Start()
    {
       /* main = Camera.main.transform;
        if (GetComponent<Renderer>() != null)
        {
            renderer1 = GetComponent<Renderer>();
        }
 
        materials = renderer1.materials;
        for (int i = 0; i < materials.Length; i++)
        {
            Color cc = materials[i].GetColor("_Color");

            materials[i].SetColor("_Color", new Color(cc.r,cc.g,cc.b,0.5f));
            //materials[i].SetColor("Albedo", new Color(cc.r, cc.g, cc.b, 255 * Vector3.Distance(transform.position, main.position)));
        }*/
    }
    private void Update()
    {
        /*if (Vector3.Distance(transform.position,main.position)<= 1)
        {

        }*/
    }
}
