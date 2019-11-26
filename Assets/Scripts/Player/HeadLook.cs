using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour {
    private Animator animator;
    public Transform LookTransform;
    public Vector3 lookOffset;
    public bool Look;
    private void Start()
    {

        animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (Look)
        {
            animator.SetLookAtWeight(1,1,1,1,1);
        }
        else
        {

            animator.SetLookAtWeight(0,0,0,0,0);
        }
        animator.SetLookAtPosition(LookTransform.position+lookOffset    );
    }
}
