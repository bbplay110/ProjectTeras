using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour {
    public Transform pivot, botton, middle, top,tip;
    public Vector3 target = Vector3.forward;
    public Vector3 normal = Vector3.up;
    float bottonLength, middleLenght, topLengt,pivotLength;
    Vector3 topTarget, tipTarget;
    private void Reset()
    {

        pivot = transform;
        try
        {
            botton = pivot.GetChild(0);
            middle = botton.GetChild(1);
            top = middle.GetChild(1);
            tip = top.GetChild(1);
        }
        catch (UnityException)
        {
            Debug.LogWarning("CantFindRequireTransform!");
        }
       

        
    }
    // Use this for initialization
    private void Awake()
    {
        bottonLength = (middle.position - botton.position).magnitude;
        middleLenght = (top.position - middle.position).magnitude;
        topLengt = (tip.position - top.position).magnitude;
        pivotLength = (pivot.position - botton.position).magnitude;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tipTarget = target;
        topTarget = target + normal * topLengt;
        Solve();
	}
    void Solve()
    {
        var pivotDir = topTarget - pivot.position;
        pivot.rotation = Quaternion.LookRotation(pivotDir);
        var BottonToTarget = topTarget - botton.position;
        var a = bottonLength;
        var b = middleLenght;
        var c = BottonToTarget.magnitude;

        var B = Mathf.Acos((c * c + a * a - b * b) / (2 * c * a)) * Mathf.Rad2Deg;
        var C= Mathf.Acos((a*a+b*b-c*c) / (2 * a * b)) * Mathf.Rad2Deg;
    }
}
