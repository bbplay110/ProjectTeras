using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBomb : MonoBehaviour {
    public GameObject corrosionGround;
    public bool isShininh;
    public float shiningRate;
    public GameObject shiningObject;
    private bool tmpShining=false;
    private bool isPaused;
    private bool pauseOnStart = false;
    private Rigidbody rigi;
    private Vector3 tempVelocity;
    private void Start()
    {
        isPaused = GameObject.Find("Player").GetComponent<bulletTime>().IsPaused;
        rigi = GetComponent<Rigidbody>();
        bulletTime.OnPauseTime += onPauseEvent84;
        bulletTime.UnPauseTime += unPauseEvent84;
        if (isPaused)
        {

            this.onPauseEvent84();
            pauseOnStart = true;
        }
        if (isShininh)
        {
            InvokeRepeating("Shining", 0, shiningRate);
        }
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onPauseEvent84;
        bulletTime.UnPauseTime -= unPauseEvent84;
    }
    void onPauseEvent84()
    {
        isPaused = true;
        GetComponent<Collider>().enabled = false;
        tempVelocity = rigi.velocity;
        rigi.Sleep();
    }
    void unPauseEvent84()
    {
        isPaused = false;
        GetComponent<Collider>().enabled = true;
        rigi.WakeUp();
        if (pauseOnStart)
        {
            rigi.AddForce(transform.forward * 3, ForceMode.Impulse);
            pauseOnStart = false;
        }
        else
        {
            rigi.AddForce(tempVelocity);
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
            bulletTime.OnPauseTime -= onPauseEvent84;
            bulletTime.UnPauseTime -= unPauseEvent84;
            Destroy(gameObject);
        }
        
    }
}
