using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MegmentOrb : MonoBehaviour {
    public magnetic Magnetic;
    public Transform FollowObject;
    private Material MagmentN, MagmentS;
    // Use this for initialization
    void Start ()
    {
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
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Magment"&&other.GetComponent<Megnetic>()!=null)
        {
            magnetic otherMagnetic= other.GetComponent<Megnetic>().Magnetic;
            switch (otherMagnetic)
            {
                case magnetic.south:
                    if (Magnetic == magnetic.north)
                    {

                        iTween.MoveUpdate(other.gameObject, transform.position, 2);
                    }
                    else if (Magnetic == magnetic.south)
                    {
                        iTween.MoveUpdate(other.gameObject,  other.transform.position- transform.position, 2);
                    }
                    break;
                case magnetic.north:
                    if (Magnetic == magnetic.north)
                    {
                        iTween.MoveUpdate(other.gameObject,other.transform.position- transform.position, 2);
                    }
                    else if(Magnetic == magnetic.south)
                    {
                        iTween.MoveUpdate(other.gameObject,transform.position, 2);
                    }
                    break;
                default:
                    break;
            }

            Debug.Log("Magment!");
        }
    }
}
