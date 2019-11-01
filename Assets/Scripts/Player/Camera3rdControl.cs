﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera3rdControl : MonoBehaviour
{
    public Transform target; //跟隨目標
    public Transform OnAimTarget;
    private Transform currentTarget;
    private Transform followPoint; //插植,實際上camera會待的位置
    public float distence, disSpeed, minDistence, maxDistence; //現在攝影機離玩家的距離,變換距離的時間,離玩家的最大跟最小距離
    protected float x; 
    protected float y;
    public float followTime = 1f;
    private float xSpeed = 1;
    private float ySpeed = 1;
    public bool inverb = false;
    public float Sensitivity=2;
    public bool bOpenRay = true;
    private float HitDistance = 0f;
    private bool bHit = false;
    private float AdjuGap = 4f;
    private Vector3 cameraPosition;
    private Quaternion rotationEuler;
    private int SceneObjectLayer;
    public float YRotate=30;
    // Use this for initialization
    void Start()
    {
        currentTarget = target;
        shooter.onAim += startAim;
        shooter.unAim += AimEnd;
        Sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100);
        //string isInverb = PlayerPrefs.GetString("MouseInverb", "false");
        //inverb = bool.Parse(isInverb);
        GameObject FollowPoint = new GameObject();
        
        FollowPoint.transform.localPosition = Vector3.zero;
        FollowPoint.name = "CameraFollowPoint";
        followPoint = FollowPoint.transform;
        followPoint.transform.position = currentTarget.position;
        //gameObject.transform.position = followPoint.position;
        SceneObjectLayer = LayerMask.GetMask("SceneObject");
    }
    void startAim()
    {
        distence = minDistence;
        currentTarget = OnAimTarget;
    }
    void AimEnd()
    {
        distence = maxDistence;
        currentTarget = target;
    }
    // Update is called once per frame
    void LateUpdate()
    {

        if(hInput.GetAxis("Mouse X")!=0|| hInput.GetAxis("Mouse Y") != 0) {
            x += hInput.GetAxis("Mouse X") * xSpeed*Sensitivity * Time.deltaTime;
            if (!inverb)
            {
                y -= hInput.GetAxis("Mouse Y") * ySpeed * Sensitivity * Time.deltaTime;
            }
            else
            {
                y += hInput.GetAxis("Mouse Y") * ySpeed * Sensitivity * Time.deltaTime;
            }
        }
        if (hInput.GetAxis("Mouse X") ==0  || hInput.GetAxis("Mouse Y") == 0)
        {
            x += hInput.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y += hInput.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        }

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
        iTween.MoveUpdate(followPoint.gameObject, currentTarget.position, followTime);
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
    private void OnDestroy()
    {
        shooter.onAim -= startAim;
        shooter.unAim -= AimEnd;
    }
}
