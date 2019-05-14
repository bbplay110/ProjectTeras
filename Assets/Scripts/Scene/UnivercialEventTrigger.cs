using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UnivercialEventTrigger : MonoBehaviour {
    public string[] targetTag;
    public UnityEvent stay;
    public UnityEvent exit;
    public UnityEvent enter;

    // Use this for initialization


    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in targetTag)
        {
            if (other.tag == item)
            {
                enter.Invoke();    
            }

        }
        
    }
    private void OnTriggerStay(Collider collision)
    {
        foreach (var item in targetTag)
        {
            if (collision.tag == item)
            {
                stay.Invoke();
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var item in targetTag)
        {
            if (other.tag == item)
            {
                exit.Invoke();
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
