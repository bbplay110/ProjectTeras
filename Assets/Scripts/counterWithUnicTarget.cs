using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class counterWithUnicTarget : counter {
    //public GameObject[] Target;
    public List<GameObject> targets;
    public UnityEvent OnTargetNull;
    public bool CountAfterTargetNull;
    private int targetCount;
	// Use this for initialization
	void Start () {
        hurt.onDied += CountTarget;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    override protected void check()
    {
        foreach (var item in targets)
        {
            if (item==null)
            {
                Debug.Log("null!");
                targets.Remove(item);
            }
        }
        if (targets == null)
        {
            OnTargetNull.Invoke();
            if (!CountAfterTargetNull)
            {
                Destroy(gameObject);
            }
        }
        if (count == 0 && CountAfterTargetNull)
        {
            onCount0.Invoke();

            Destroy(gameObject);
        }
    }
    void CountTarget()
    {
        //Debug.Log(hurt.onDied.se);

        foreach(var item in targets)
        {
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !IsStart)
        {
            IsStart = true;
            onStart.Invoke();
            if(CountAfterTargetNull)
                hurt.onDied += countMinus;
        }
    }

}
