using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum magnetic { north, south,none }
public class MegmentCreater : MonoBehaviour {
    public Material MagmentN, MagmentS, MagmentNone;
    public GameObject orb;
    public magnetic magnetic;
       
    // Use this for initialization
    void Start () {
        //MagmentN = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/MagmentN.mat");
        //MagmentS = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/MagmentS.mat");

       // MagmentNone = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/MagmentNone.mat");
        switch (magnetic)
        {
            case magnetic.north:
                GetComponent<MeshRenderer>().material =MagmentN ;
                
                break;

            case magnetic.south:

                GetComponent<MeshRenderer>().material = MagmentS;
                break;
            case magnetic.none:

                GetComponent<MeshRenderer>().material = MagmentNone;
                break;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Magment") {
            GameObject oorb;
        switch (magnetic)
        {
            case magnetic.north:

                if (other.transform.Find("MagmentOrb(Clone)") != null)
                    Destroy(other.transform.Find("MagmentOrb(Clone)").gameObject, 0);
                oorb = Instantiate(orb,other.transform.position,other.transform.rotation, other.transform)as GameObject;
                oorb.GetComponent<MegmentOrb>().Magnetic = magnetic;
                    oorb.transform.localScale = new Vector3(oorb.transform.localScale.x / other.transform.localScale.x, oorb.transform.localScale.y / other.transform.localScale.y, oorb.transform.localScale.z / other.transform.localScale.z);


                    Debug.Log("LocalScale" + oorb.transform.localScale);
                    break;
            case magnetic.south:
                if (other.transform.Find("MagmentOrb(Clone)") != null)
                    Destroy(other.transform.Find("MagmentOrb(Clone)").gameObject, 0);
                oorb = Instantiate(orb, other.transform.position, other.transform.rotation, other.transform) as GameObject;
                oorb.GetComponent<MegmentOrb>().Magnetic = magnetic;
                    oorb.transform.localScale = new Vector3(oorb.transform.localScale.x / other.transform.localScale.x, oorb.transform.localScale.y / other.transform.localScale.y, oorb.transform.localScale.z / other.transform.localScale.z);
                    Debug.Log("LocalScale"+oorb.transform.localScale);
                break;
            case magnetic.none:
                if (other.transform.Find("MagmentOrb(Clone)") != null)
                    Destroy(other.transform.Find("MagmentOrb(Clone)").gameObject, 0);
                break;
        }
        }
    }
}
