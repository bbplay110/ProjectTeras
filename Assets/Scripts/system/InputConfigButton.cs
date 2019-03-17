using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputConfigButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<Text>().text = hInput.DetailsFromKey(gameObject.name, KeyTarget.PositivePrimary).ToString();
	}
    private void OnEnable()
    {
        gameObject.GetComponentInChildren<Text>().text = hInput.DetailsFromKey(gameObject.name, KeyTarget.PositivePrimary).ToString();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
