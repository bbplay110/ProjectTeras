using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTester : MonoBehaviour {
    private void OnDestroy()
    {
        Debug.Log("Fuck!"+gameObject.name+"is destroyed!");
    }
}
