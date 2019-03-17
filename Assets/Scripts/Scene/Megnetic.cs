using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Megnetic : MonoBehaviour {
    public magnetic Magnetic;

    private Material MagmentN, MagmentS;
    // Use this for initialization
    void Start () {
        MagmentN = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/MagmentN.mat");
        MagmentS = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/MagmentS.mat");
        switch (Magnetic)
        {
            case magnetic.north:
                GetComponent<MeshRenderer>().material = MagmentN;

                break;

            case magnetic.south:

                GetComponent<MeshRenderer>().material = MagmentS;
                break;

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0)
        {
            Destroy(gameObject, 0);
        }
	}
}
