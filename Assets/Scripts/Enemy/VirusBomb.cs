using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBomb : MonoBehaviour {
    public GameObject corrosionGround;
    public bool isShininh;
    public float shiningRate;
    public GameObject shiningObject;
    private bool tmpShining=false;
    private void Start()
    {
        if (isShininh)
        {
            InvokeRepeating("Shining", 0, shiningRate);
        }
    }
    void Shining()
    {
        tmpShining = !tmpShining;
        shiningObject.SetActive(tmpShining);
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
