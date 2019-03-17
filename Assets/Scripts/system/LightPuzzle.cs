using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public bool Light = false;
    public GameObject light1, light2;
    private GameObject effect;
    public GameObject Tip;
    // Use this for initialization
    void Start()
    {
        Tip = transform.Find("Tip").gameObject;
        effect = transform.Find("Particle System").gameObject;
        switch (Light)
        {
            case true:
                effect.SetActive(true);
                break;
            case false:
                effect.SetActive(false);
                break;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Submit"))
            {
                light1.GetComponent<LightPuzzle>().SwitchLightSelf();
                light2.GetComponent<LightPuzzle>().SwitchLightSelf();
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Tip.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            Tip.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        switch (Light)
        {
            case true:
                effect.SetActive(true);
                break;
            case false:
                effect.SetActive(false);
                break;
        }
    }
    void SwitchLightSelf()
    {
        Light = !Light;
    }
}