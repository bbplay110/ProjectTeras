using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnivercialEventTrigger : MonoBehaviour {
    public string[] targetTag;
    public UnityEvent stay;
    public UnityEvent exit;
    public UnityEvent enter;
    // Use this for initialization
    void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in targetTag)
        {
            if (other.tag == item)
            {
                enter.Invoke();
                Debug.Log("TriggerEnter!");
            }

        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        foreach (var item in targetTag)
        {
            if (collision.collider.tag == item)
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
