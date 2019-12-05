using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShowUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
	}
}
