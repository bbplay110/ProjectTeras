using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3rdControl : MonoBehaviour
{
    public Transform target;
    private Transform followPoint;
    public float distence, disSpeed, minDistence, maxDistence;
    protected float x;
    protected float y;
    public float followSpeed = 2.0f;
    public float xSpeed = 1;
    public float ySpeed = 1;
    public bool bOpenRay = true;
    private float HitDistance = 0f;
    private bool bHit = false;
    private float AdjuGap = 0.1f;
    private Vector3 cameraPosition;
    private Quaternion rotationEuler;
    private int SceneObjectLayer;
    public float YRotate=30;
    // Use this for initialization
    void Start()
    {
        GameObject FollowPoint = new GameObject();
        FollowPoint.transform.localPosition = Vector3.zero;
        FollowPoint.name = "CameraFollowPoint";
        followPoint = FollowPoint.transform;
        SceneObjectLayer = LayerMask.GetMask("SceneObject");
    }

    // Update is called once per frame
    void LateUpdate()
    {


        x += hInput.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        
        y += hInput.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        
        if (y > YRotate)
        {
            y = YRotate;
        }
        else if (y < -YRotate)
        {
            y = -YRotate;
        }
        distence -= hInput.GetAxis("Mouse ScrollWheel") * disSpeed * Time.deltaTime;
        distence = Mathf.Clamp(distence, minDistence, maxDistence);
        rotationEuler = Quaternion.Euler(y, x, 0);
        if (bHit == false)
        {
            cameraPosition = rotationEuler * new Vector3(0, 0, -distence) + followPoint.position;
        }
        transform.rotation = rotationEuler;
        transform.position = cameraPosition;
        Fungus.iTween.MoveUpdate(followPoint.gameObject, target.position, followSpeed);
        if (bOpenRay)
        {
            AutoRegulationPos();
        }

    }
    void AutoRegulationPos()
    {
        RaycastHit hit;
        float tmpValue = distence;
        if (bHit == false)
        {
            tmpValue = distence;
        }
        if (Physics.Raycast(followPoint.position, gameObject.transform.TransformDirection(Vector3.back), out hit, tmpValue,SceneObjectLayer))
        {

            bHit = true;
            HitDistance = -Vector3.Distance(followPoint.position, hit.point) + AdjuGap;
            tmpValue = HitDistance;
            cameraPosition = rotationEuler * new Vector3(0, 0, HitDistance) + followPoint.position + new Vector3(0, 2, 0);
        }
        else if (bHit)
        {
            bHit = false;
            HitDistance = 0.0f;
        }

    }
}
