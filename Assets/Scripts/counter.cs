using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class counter : MonoBehaviour {
    public UnityEvent onCount0;
    public UnityEvent onStart;
    public int count;
    public Text countText;
    private bool isStart;


    public bool IsStart
    {
        get
        {
            return isStart;
        }

        set
        {
            isStart = value;
        }
    }

    // Use this for initialization
    void Start() {
        GetComponent<MeshRenderer>().enabled = false;
        if (count == 0)
        {
            Debug.LogWarning("Counter Is 0!");
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&!IsStart)
        {
            IsStart = true;
            onStart.Invoke();
            hurt.onDied += countMinus;
        }
    }

    public void countMinus(){
        if (IsStart)
        {
            if(countText!=null)
                countText.text = ""+count;
            count--;
        }
    }
    public void countPlus()
    {
        if (IsStart)
        {
            if (countText != null)
                countText.text = "" + count;
            count++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        check();
	}
    protected virtual void check()
    {
        if (count == 0)
        {
            onCount0.Invoke();
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        hurt.onDied -= countMinus;
    }
}
