/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{

    #region Variables 
    public Transform target;
    public Vector3 offset;
    bool smoothFollowAllowed = true;
    public float smoothTime = 0.125f;
    Vector3 cameraTargetVelocity;


    #endregion

    #region Unity Functions

    void Start()
    {

    }

    void LateUpdate()
    {
        cameraTargetVelocity = target.transform.position + offset;
        if (smoothFollowAllowed)
        {
            transform.position = Vector3.Lerp(transform.position, cameraTargetVelocity, smoothTime);
        }
        else
            transform.position = cameraTargetVelocity;

    }

    #endregion

    #region UserDefined

    #endregion

}
