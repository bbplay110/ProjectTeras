using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : MonoBehaviour {
    public Transform OriginalParent
    {
        get;
        set;
    }

    void Awake()
    {
        this.OriginalParent = this.transform.parent;
    }
    // Use this for initialization
}
