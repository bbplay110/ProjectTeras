using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTest : MonoBehaviour
{

    public GameObject TriggerZone;
    bool KnowTutorial = false;


    private void Awake()
    {
        if(KnowTutorial == true)
        { 
            TriggerZone.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TriggerZone.SetActive(true);
            KnowTutorial = true;
        }
    }

    public void CloseTurorial()
    {
        if (KnowTutorial == true)
        {
            TriggerZone.SetActive(false);
        }

    }
}